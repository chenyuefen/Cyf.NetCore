using Dapper;
using DapperExtensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TM.Core.Data.Dapper
{
    /// <summary>
    /// 数据库链接总线
    /// </summary>
    public class ConnectionBase : IConnectionBase
    {
        #region Member Fields
        private IConfiguration _cfg;
        private string _connectionString;
        private IDbProvider _provider;
        private IDbConnection _sharedConnection;
        private int _sharedConnectionDepth;
        private DbProviderFactory _factory;

        public IDbTransaction Trans;
        //private IDbTransaction _transaction;
        private int _transactionDepth;
        private bool _transactionCancelled;
        private IsolationLevel? _isolationLevel;
        private string _paramPrefix;
        private string _lastSql;
        private object _lastArgs;
        private static readonly ConcurrentDictionary<Type, List<PropertyInfo>> _paramCache = new ConcurrentDictionary<Type, List<PropertyInfo>>();
        private DataSource _dataSource;

        #endregion

        #region IDisposable

        /// <summary>
        ///     Automatically close one open shared connection
        /// </summary>
        public void Dispose()
        {
            // Automatically close one open connection reference
            //  (Works with KeepConnectionAlive and manually opening a shared connection)
            CloseSharedConnection();
        }

        #endregion

        #region Constructors
        public ConnectionBase(IConfiguration cfg)
        {
            _cfg = cfg;
            _connectionString = _cfg.GetConnectionString("ConnectionStrings:MasterDb");
            Initialise(DatabaseProvider.Resolve("SqlServer", true, _connectionString));
        }

        public ConnectionBase(string connectionString, string providerName = null)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("Connection string cannot be null or empty", "connectionString");

            _connectionString = connectionString;
            Initialise(DatabaseProvider.Resolve((providerName ?? "SqlServer"), true, _connectionString));
            //_connectionString = connectionString;
        }

        /// <summary>
        ///     Constructs an instance using a supplied IDbConnection.
        /// </summary>
        /// <param name="connection">The IDbConnection to use.</param>
        /// <remarks>
        ///     The supplied IDbConnection will not be closed/disposed by PetaPoco - that remains
        ///     the responsibility of the caller.
        /// </remarks>
        /// <exception cref="ArgumentException">Thrown when <paramref name="connection" /> is null or empty.</exception>
        public ConnectionBase(IDbConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");

            _sharedConnection = connection;
            _connectionString = connection.ConnectionString;
            // Prevent closing external connection
            _sharedConnectionDepth = 2;

            Initialise(DatabaseProvider.Resolve(_sharedConnection.GetType(), false, _connectionString));
        }

        /// <summary>
        ///     Provides common initialization for the various constructors.
        /// </summary>
        public void Initialise(IDbConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");

            _sharedConnection = connection;
            _connectionString = connection.ConnectionString;
            // Prevent closing external connection
            _sharedConnectionDepth = 2;

            Initialise(DatabaseProvider.Resolve(_sharedConnection.GetType(), false, _connectionString));
        }

        /// <summary>
        ///     Provides common initialization for the various constructors.
        /// </summary>
        private void Initialise(IDbProvider provider)
        {
            // Reset
            // What character is used for delimiting parameters in SQL
            _provider = provider;
            _paramPrefix = _provider.GetParameterPrefix(_connectionString);
            _factory = _provider.GetFactory();
            //_defaultMapper = mapper ?? new ConventionMapper();
        }

        #endregion

        #region Public Properties
        /// <summary>
        ///     When set to true, PetaPoco will automatically create the "SELECT columns" part of any query that looks like it
        ///     needs it
        /// </summary>
        public bool EnableAutoSelect { get; set; }

        /// <summary>
        ///     When set to true, parameters can be named ?myparam and populated from properties of the passed in argument values.
        /// </summary>
        public bool EnableNamedParams { get; set; }

        /// <summary>
        ///     Sets the timeout value for all SQL statements.
        /// </summary>
        public int CommandTimeout { get; set; }

        /// <summary>
        ///     Sets the timeout value for the next (and only next) SQL statement
        /// </summary>
        public int OneTimeCommandTimeout { get; set; }

        /// <summary>
        ///     Gets the loaded database provider. <seealso cref="Provider" />.
        /// </summary>
        /// <returns>
        ///     The loaded database type.
        /// </returns>
        public IDbProvider Provider
        {
            get { return _provider; }
        }

        /// <summary>
        ///     Gets the connection string.
        /// </summary>
        /// <returns>
        ///     The connection string.
        /// </returns>
        public string ConnectionString
        {
            get { return _connectionString; }
        }

        /// <summary>
        ///     Gets or sets the transaction isolation level.
        /// </summary>
        /// <remarks>
        ///     When value is null, the underlying providers default isolation level is used.
        /// </remarks>
        public IsolationLevel? IsolationLevel
        {
            get { return _isolationLevel; }
            set
            {
                if (Trans != null)
                    throw new InvalidOperationException("Isolation level can't be changed during a transaction.");

                _isolationLevel = value;
            }
        }

        #endregion

        #region Transaction Management

        /// <summary>
        ///     Gets the current transaction instance.
        /// </summary>
        /// <returns>
        ///     The current transaction instance; else, <c>null</c> if not transaction is in progress.
        /// </returns>
        IDbTransaction ITransactionAccessor.Transaction
        {
            get { return Trans; }
        }

        // Helper to create a transaction scope

        /// <summary>
        ///     Starts or continues a transaction.
        /// </summary>
        /// <returns>An ITransaction reference that must be Completed or disposed</returns>
        /// <remarks>
        ///     This method makes management of calls to Begin/End/CompleteTransaction easier.
        ///     The usage pattern for this should be:
        ///     using (var tx = db.GetTransaction())
        ///     {
        ///     // Do stuff
        ///     db.Update(...);
        ///     // Mark the transaction as complete
        ///     tx.Complete();
        ///     }
        ///     Transactions can be nested but they must all be completed otherwise the entire
        ///     transaction is aborted.
        /// </remarks>
        public ITransaction GetTransaction()
        {
            return new Transaction(this);
        }

        /// <summary>
        ///     Called when a transaction starts.  Overridden by the T4 template generated database
        ///     classes to ensure the same DB instance is used throughout the transaction.
        /// </summary>
        public virtual void OnBeginTransaction()
        {
        }

        /// <summary>
        ///     Called when a transaction ends.
        /// </summary>
        public virtual void OnEndTransaction()
        {
        }

        /// <summary>
        ///     Starts a transaction scope, see GetTransaction() for recommended usage
        /// </summary>
        public void BeginTransaction()
        {
            _transactionDepth++;

            if (_transactionDepth == 1)
            {
                OpenSharedConnection();
                Trans = !_isolationLevel.HasValue ? _sharedConnection.BeginTransaction() : _sharedConnection.BeginTransaction(_isolationLevel.Value);
                _transactionCancelled = false;
                OnBeginTransaction();
            }
        }

        /// <summary>
        ///     Internal helper to cleanup transaction
        /// </summary>
        private void CleanupTransaction()
        {
            OnEndTransaction();

            if (_transactionCancelled)
                Trans.Rollback();
            else
                Trans.Commit();

            Trans.Dispose();
            Trans = null;

            CloseSharedConnection();
        }

        /// <summary>
        ///     Aborts the entire outer most transaction scope
        /// </summary>
        /// <remarks>
        ///     Called automatically by Transaction.Dispose()
        ///     if the transaction wasn't completed.
        /// </remarks>
        public void AbortTransaction()
        {
            _transactionCancelled = true;
            if ((--_transactionDepth) == 0)
                CleanupTransaction();
        }

        /// <summary>
        ///     Marks the current transaction scope as complete.
        /// </summary>
        public void CompleteTransaction()
        {
            if ((--_transactionDepth) == 0)
                CleanupTransaction();
        }

        /// <summary>
        ///     Transaction object helps maintain transaction depth counts
        /// </summary>
        public class Transaction : ITransaction
        {
            private ConnectionBase _db;

            public Transaction(ConnectionBase db)
            {
                _db = db;
                _db.BeginTransaction();
            }

            public void Complete()
            {
                _db.CompleteTransaction();
                _db = null;
            }

            public void Dispose()
            {
                if (_db != null)
                    _db.AbortTransaction();
            }
        }

        #endregion

        #region Connection Management

        //protected DataSource DataSource
        //{
        //    get { return "" ); }
        //}

        /// <summary>
        ///     When set to true the first opened connection is kept alive until this object is disposed
        /// </summary>
        public bool KeepConnectionAlive { get; set; }

        /// <summary>
        ///     Open a connection that will be used for all subsequent queries.
        /// </summary>
        /// <remarks>
        ///     Calls to Open/CloseSharedConnection are reference counted and should be balanced
        /// </remarks>
        public void OpenSharedConnection()
        {
            if (_sharedConnectionDepth == 0)
            {
                _sharedConnection = _factory.CreateConnection();
                _sharedConnection.ConnectionString = _connectionString;

                if (_sharedConnection.State == ConnectionState.Broken)
                    _sharedConnection.Close();

                if (_sharedConnection.State == ConnectionState.Closed)
                    _sharedConnection.Open();

                _sharedConnection = OnConnectionOpened(_sharedConnection);

                if (KeepConnectionAlive)
                    _sharedConnectionDepth++; // Make sure you call Dispose
            }
            _sharedConnectionDepth++;
        }

        /// <summary>
        ///     Releases the shared connection
        /// </summary>
        public void CloseSharedConnection()
        {
            if (_sharedConnectionDepth > 0)
            {
                _sharedConnectionDepth--;
                if (_sharedConnectionDepth == 0)
                {
                    OnConnectionClosing(_sharedConnection);
                    _sharedConnection.Dispose();
                    _sharedConnection = null;
                }
            }
        }

        /// <summary>
        ///     Provides access to the currently open shared connection (or null if none)
        /// </summary>
        public IDbConnection Connection
        {
            get { return _sharedConnection; }
        }


        #endregion

        #region Last Command

        /// <summary>
        ///     Retrieves the SQL of the last executed statement
        /// </summary>
        public string LastSQL
        {
            get { return _lastSql; }
        }

        /// <summary>
        ///     Retrieves the arguments to the last execute statement
        /// </summary>
        public object LastArgs
        {
            get { return _lastArgs; }
        }

        /// <summary>
        ///     Returns a formatted string describing the last executed SQL statement and it's argument values
        /// </summary>
        public string LastCommand
        {
            get { return FormatCommand(_lastSql, _lastArgs); }
        }

        #endregion

        #region FormatCommand

        /// <summary>
        ///     Formats the contents of a DB command for display
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public string FormatCommand(IDbCommand cmd)
        {
            return FormatCommand(cmd.CommandText, (from IDataParameter parameter in cmd.Parameters select parameter.Value));
        }

        /// <summary>
        ///     Formats an SQL query and it's arguments for display
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string FormatCommand(string sql, object args)
        {
            var sb = new StringBuilder();
            if (sql == null)
                return "";
            sb.Append(sql);
            if (args.GetType().IsArray)
            {
                var arrArgs = args as object[];
                if (arrArgs != null && arrArgs.Length > 0)
                {
                    sb.Append("\n");
                    for (int i = 0; i < arrArgs.Length; i++)
                    {
                        sb.AppendFormat("\t -> {0}{1} [{2}] = \"{3}\"\n", _paramPrefix, i, arrArgs[i].GetType().Name, arrArgs[i]);
                    }
                    sb.Remove(sb.Length - 1, 1);
                }
                return sb.ToString();
            }
            return sb.ToString();
        }

        #endregion

        #region Exception Reporting and Logging

        /// <summary>
        ///     Called if an exception occurs during processing of a DB operation.  Override to provide custom logging/handling.
        /// </summary>
        /// <param name="x">The exception instance</param>
        /// <returns>True to re-throw the exception, false to suppress it</returns>
        public virtual bool OnException(string operation, Exception ex)
        {
            //Log4NetHelper.WriteError.Log(Utility.Enums.LogTypeEnum.SysLog, Utility.Enums.LogLevelEnum.Error, "ConnectionBase执行db[" + operation + "]异常", ex);
            System.Diagnostics.Debug.WriteLine(ex.ToString());
            System.Diagnostics.Debug.WriteLine(LastCommand);
            return true;
        }

        /// <summary>
        ///     Called when DB connection opened
        /// </summary>
        /// <param name="conn">The newly opened IDbConnection</param>
        /// <returns>The same or a replacement IDbConnection</returns>
        /// <remarks>
        ///     Override this method to provide custom logging of opening connection, or
        ///     to provide a proxy IDbConnection.
        /// </remarks>
        public virtual IDbConnection OnConnectionOpened(IDbConnection conn)
        {
            return conn;
        }

        /// <summary>
        ///     Called when DB connection closed
        /// </summary>
        /// <param name="conn">The soon to be closed IDBConnection</param>
        public virtual void OnConnectionClosing(IDbConnection conn)
        {
        }

        /// <summary>
        ///     Called just before an DB command is executed
        /// </summary>
        /// <param name="cmd">The command to be executed</param>
        /// <remarks>
        ///     Override this method to provide custom logging of commands and/or
        ///     modification of the IDbCommand before it's executed
        /// </remarks>
        public virtual void OnExecutingCommand(IDbCommand cmd)
        {
        }

        /// <summary>
        ///     Called on completion of command execution
        /// </summary>
        /// <param name="cmd">The IDbCommand that finished executing</param>
        public virtual void OnExecutedCommand(IDbCommand cmd)
        {
        }

        /// <summary>
        ///  do some things pre excute
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        internal void DoPreExecute(string sql, object param = null)
        {
            // Save it
            _lastSql = sql;
            _lastArgs = param;
        }

        #endregion

        #region IProvider
        /// <summary>
        ///     Look at the type and provider name being used and instantiate a suitable DatabaseType instance.
        /// </summary>
        /// <param name="providerName">The provider name.</param>
        /// <param name="allowDefault">A flag that when set allows the default <see cref="SqlServerDatabaseProvider"/> to be returned if not match is found.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>The database type.</returns>
        internal static IDbProvider Resolve(string providerName, bool allowDefault, string connectionString)
        {
            // Try again with provider name
            if (providerName.IndexOf("SqlServer", StringComparison.InvariantCultureIgnoreCase) >= 0 ||
               providerName.IndexOf("System.Data.SqlClient", StringComparison.InvariantCultureIgnoreCase) >= 0)
                return Singleton<SqlServerDatabaseProvider>.Instance;

            if (providerName.IndexOf("MySql", StringComparison.InvariantCultureIgnoreCase) >= 0)
                return Singleton<MySqlDatabaseProvider>.Instance;
            //if (providerName.IndexOf("MariaDb", StringComparison.InvariantCultureIgnoreCase) >= 0)
            //    return Singleton<MariaDbDatabaseProvider>.Instance;
            //if (providerName.IndexOf("SqlServerCe", StringComparison.InvariantCultureIgnoreCase) >= 0 ||
            //    providerName.IndexOf("SqlCeConnection", StringComparison.InvariantCultureIgnoreCase) >= 0)
            //    return Singleton<SqlServerCEDatabaseProviders>.Instance;
            //if (providerName.IndexOf("Npgsql", StringComparison.InvariantCultureIgnoreCase) >= 0
            //    || providerName.IndexOf("pgsql", StringComparison.InvariantCultureIgnoreCase) >= 0)
            //    return Singleton<PostgreSQLDatabaseProvider>.Instance;
            if (providerName.IndexOf("Oracle", StringComparison.InvariantCultureIgnoreCase) >= 0)
                return Singleton<OracleDatabaseProvider>.Instance;
            //if (providerName.IndexOf("SQLite", StringComparison.InvariantCultureIgnoreCase) >= 0)
            //    return Singleton<SQLiteDatabaseProvider>.Instance;
            //if (providerName.IndexOf("Firebird", StringComparison.InvariantCultureIgnoreCase) >= 0 ||
            //    providerName.IndexOf("FbConnection", StringComparison.InvariantCultureIgnoreCase) >= 0)
            //    return Singleton<FirebirdDbDatabaseProvider>.Instance;
            //if (providerName.IndexOf("OleDb", StringComparison.InvariantCultureIgnoreCase) >= 0
            //    && (connectionString.IndexOf("Jet.OLEDB", StringComparison.InvariantCultureIgnoreCase) > 0 || connectionString.IndexOf("ACE.OLEDB", StringComparison.InvariantCultureIgnoreCase) > 0))
            //{
            //    return Singleton<MsAccessDbDatabaseProvider>.Instance;
            //}

            if (!allowDefault)
                throw new ArgumentException("Could not match `" + providerName + "` to a provider.", "providerName");

            // Assume SQL Server
            return Singleton<SqlServerDatabaseProvider>.Instance;
        }

        #endregion

        #region Operation
        public dynamic Insert<T>(T ob, int? commandTimeout = null) where T : class
        {
            OpenSharedConnection();
            try
            {
                DoPreExecute("Insert", ob);
                return _sharedConnection.Insert<T>(ob, Trans, commandTimeout);
            }
            catch (Exception ex)
            {
                if (OnException("Insert", ex))
                    return null;
                return null;
            }
            finally
            {
                CloseSharedConnection();
            }
        }
        public async Task<dynamic> InsertAsync<T>(T ob, int? commandTimeout = null) where T : class
        {
            OpenSharedConnection();
            try
            {
                DoPreExecute("InsertAsync", ob);
                return await _sharedConnection.InsertAsync<T>(ob, Trans, commandTimeout);
            }
            catch (Exception ex)
            {
                if (OnException("InsertAsync", ex))
                    return null;
                return null;
            }
            finally
            {
                CloseSharedConnection();
            }
        }
        public bool Update<T>(T ob, int? commandTimeout = null, bool ignoreAllKeyProperties = false) where T : class
        {
            OpenSharedConnection();
            try
            {
                DoPreExecute("Update", ob);
                return _sharedConnection.Update<T>(ob, Trans, commandTimeout, ignoreAllKeyProperties);
            }
            catch (Exception ex)
            {
                if (OnException("Update", ex))
                    return false;
                return false;
            }
            finally
            {
                CloseSharedConnection();
            }
        }
        public async Task<bool> UpdateAsync<T>(T ob, int? commandTimeout = null, bool ignoreAllKeyProperties = false) where T : class
        {
            OpenSharedConnection();
            try
            {
                DoPreExecute("UpdateAsync", ob);
                return await _sharedConnection.UpdateAsync<T>(ob, Trans, commandTimeout, ignoreAllKeyProperties);
            }
            catch (Exception ex)
            {
                if (OnException("UpdateAsync", ex))
                    return false;
                return false;
            }
            finally
            {
                CloseSharedConnection();
            }
        }
        public int Execute(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            OpenSharedConnection();
            try
            {
                DoPreExecute(sql, param);
                return _sharedConnection.Execute(sql, param, Trans, commandTimeout, commandType);
            }
            catch (Exception ex)
            {
                if (OnException("Execute", ex))
                    throw ex;
                return 0;
            }
            finally
            {
                CloseSharedConnection();
            }
        }
        public async Task<int> ExecuteAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            OpenSharedConnection();
            try
            {
                DoPreExecute(sql, param);
                return await _sharedConnection.ExecuteAsync(sql, param, Trans, commandTimeout, commandType);
            }
            catch (Exception ex)
            {
                if (OnException("ExecuteAsync", ex))
                    throw ex;
                return 0;
            }
            finally
            {
                CloseSharedConnection();
            }
        }
        public bool Delete<T>(T ob, int? commandTimeout = null) where T : class
        {
            OpenSharedConnection();
            try
            {
                DoPreExecute("Delete", ob);
                return _sharedConnection.Delete(ob, Trans, commandTimeout);
            }
            catch (Exception ex)
            {
                if (OnException("Delete", ex))
                    throw ex;
                return false;
            }
            finally
            {
                CloseSharedConnection();
            }
        }
        public async Task<bool> DeleteAsync<T>(T ob, int? commandTimeout = null) where T : class
        {
            OpenSharedConnection();
            try
            {
                DoPreExecute("DeleteAsync", ob);
                return await _sharedConnection.DeleteAsync(ob, Trans, commandTimeout);
            }
            catch (Exception ex)
            {
                if (OnException("DeleteAsync", ex))
                    throw ex;
                return false;
            }
            finally
            {
                CloseSharedConnection();
            }
        }
        public object ExecuteScalar(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            OpenSharedConnection();
            try
            {
                DoPreExecute(sql, param);
                return _sharedConnection.ExecuteScalar(sql, param, Trans, commandTimeout, commandType);
            }
            catch (Exception ex)
            {
                if (OnException("ExecuteScalar", ex))
                    throw ex;
                return 0;
            }
            finally
            {
                CloseSharedConnection();
            }
        }
        public async Task<object> ExecuteScalarAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            OpenSharedConnection();
            try
            {
                DoPreExecute(sql, param);
                return await _sharedConnection.ExecuteScalarAsync(sql, param, Trans, commandTimeout, commandType);
            }
            catch (Exception ex)
            {
                if (OnException("ExecuteScalarAsync", ex))
                    throw ex;
                return 0;
            }
            finally
            {
                CloseSharedConnection();
            }
        }
        public T ExecuteScalar<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            OpenSharedConnection();
            try
            {
                DoPreExecute(sql, param);
                return _sharedConnection.ExecuteScalar<T>(sql, param, Trans, commandTimeout, commandType);
            }
            catch (Exception ex)
            {
                if (OnException("ExecuteScalar", ex))
                    throw ex;
                return default(T);
            }
            finally
            {
                CloseSharedConnection();
            }
        }
        public async Task<T> ExecuteScalarAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            OpenSharedConnection();
            try
            {
                DoPreExecute(sql, param);
                return await _sharedConnection.ExecuteScalarAsync<T>(sql, param, Trans, commandTimeout, commandType);
            }
            catch (Exception ex)
            {
                if (OnException("ExecuteScalarAsync", ex))
                    throw ex;
                return default(T);
            }
            finally
            {
                CloseSharedConnection();
            }
        }
        public T QueryFirstOrDefault<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            OpenSharedConnection();
            try
            {
                DoPreExecute(sql, param);
                return _sharedConnection.QueryFirstOrDefault<T>(sql, param, Trans, commandTimeout, commandType);
            }
            catch (Exception ex)
            {
                if (OnException("QueryFirstOrDefault", ex))
                    throw ex;
                return default(T);
            }
            finally
            {
                CloseSharedConnection();
            }
        }
        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            OpenSharedConnection();
            try
            {
                DoPreExecute(sql, param);
                return await _sharedConnection.QueryFirstOrDefaultAsync<T>(sql, param, Trans, commandTimeout, commandType);
            }
            catch (Exception ex)
            {
                if (OnException("QueryFirstOrDefaultAsync", ex))
                    throw ex;
                return default(T);
            }
            finally
            {
                CloseSharedConnection();
            }
        }
        /// <summary>
        ///  attention to buffered that it will operate cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commanType"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql, object param = null, bool buffered = false, int? commandTimeout = null, CommandType? commanType = null)
        {
            OpenSharedConnection();
            try
            {
                DoPreExecute(sql, param);
                return _sharedConnection.Query<T>(sql, param, Trans, buffered, commandTimeout, commanType);
            }
            catch (Exception ex)
            {
                if (OnException("Query", ex))
                    throw ex;
                return null;
            }
            finally
            {
                CloseSharedConnection();
            }
        }
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commanType = null)
        {
            OpenSharedConnection();
            try
            {
                DoPreExecute(sql, param);
                return await _sharedConnection.QueryAsync<T>(sql, param, Trans, commandTimeout, commanType);
            }
            catch (Exception ex)
            {
                if (OnException("QueryAsync", ex))
                    throw ex;
                return null;
            }
            finally
            {
                CloseSharedConnection();
            }
        }

        /// <summary>Get data count from table with a specified condition.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="isOr"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public int Count(object condition, string table, bool isOr = false, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return QueryList<int>(condition, table, "count(*)", isOr, transaction, commandTimeout).Single();
        }
        /// <summary>Get data count async from table with a specified condition.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="isOr"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public Task<int> CountAsync(object condition, string table, bool isOr = false, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return QueryListAsync<int>(condition, table, "count(*)", isOr, transaction, commandTimeout).ContinueWith<int>(t => t.Result.Single());
        }

        public IEnumerable<T> GetPage<T>(object param = null, IList<ISort> sorts = null, int page = 1, int resultsPerPage = 10, int? commandTimeout = null, bool buffered = false) where T : class
        {
            OpenSharedConnection();
            try
            {
                DoPreExecute("GetPage", param);
                return _sharedConnection.GetPage<T>(param, sorts, page, resultsPerPage, Trans, commandTimeout, buffered);
            }
            catch (Exception ex)
            {
                if (OnException("GetPageAsync", ex))
                    throw ex;
                return null;
            }
            finally
            {
                CloseSharedConnection();
            }

        }
        public async Task<IEnumerable<T>> GetPageAsync<T>(object param = null, IList<ISort> sorts = null, int page = 1, int resultsPerPage = 10, int? commandTimeout = null) where T : class
        {
            OpenSharedConnection();
            try
            {
                DoPreExecute("GetPageAsync", param);
                return await _sharedConnection.GetPageAsync<T>(param, sorts, page, resultsPerPage, Trans, commandTimeout);
            }
            catch (Exception ex)
            {
                if (OnException("GetPageAsync", ex))
                    throw ex;
                return null;
            }
            finally
            {
                CloseSharedConnection();
            }

        }

        /// <summary>Query a list of data from table with a specified condition.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <param name="isOr"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> QueryList(dynamic condition, string table, string columns = "*", bool isOr = false, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return QueryList<dynamic>(condition, table, columns, isOr, transaction, commandTimeout);
        }

        /// <summary>Query a list of data async from table with a specified condition.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <param name="isOr"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public Task<IEnumerable<dynamic>> QueryListAsync(dynamic condition, string table, string columns = "*", bool isOr = false, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return QueryListAsync<dynamic>(condition, table, columns, isOr, transaction, commandTimeout);
        }

        /// <summary>Query a list of data from table with specified condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <param name="isOr"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public IEnumerable<T> QueryList<T>(object condition, string table, string columns = "*", bool isOr = false, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            OpenSharedConnection();
            try
            {
                var sql = BuildQuerySQL(condition, table, columns, isOr);
                DoPreExecute(sql, condition);
                return _sharedConnection.Query<T>(sql, condition, transaction, true, commandTimeout);
            }
            catch (Exception ex)
            {
                if (OnException("QueryList", ex))
                    throw ex;
                return null;
            }
            finally
            {
                CloseSharedConnection();
            }
        }


        /// <summary>Query a list of data async from table with specified condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <param name="isOr"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> QueryListAsync<T>(object condition, string table, string columns = "*", bool isOr = false, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            OpenSharedConnection();
            try
            {
                var sql = BuildQuerySQL(condition, table, columns, isOr);
                DoPreExecute(sql, condition);
                return await _sharedConnection.QueryAsync<T>(sql, condition, transaction, commandTimeout);
            }
            catch (Exception ex)
            {
                if (OnException("QueryListAsync", ex))
                    throw ex;
                return null;
            }
            finally
            {
                CloseSharedConnection();
            }

        }

        /// <summary>
        /// build query sql
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="selectPart"></param>
        /// <param name="isOr"></param>
        /// <returns></returns>
        private string BuildQuerySQL(dynamic condition, string table, string selectPart = "*", bool isOr = false)
        {
            var conditionObj = condition as object;
            var properties = GetProperties(conditionObj);
            if (properties.Count == 0)
            {
                return string.Format("SELECT {1} FROM {0}", table, selectPart);
            }

            var separator = isOr ? " OR " : " AND ";
            var wherePart = string.Join(separator, properties.Select(p => p + " = @" + p));

            return string.Format("SELECT {2} FROM {0} WHERE {1}", table, wherePart, selectPart);
        }
        private List<string> GetProperties(object obj)
        {
            if (obj == null)
            {
                return new List<string>();
            }
            if (obj is DynamicParameters)
            {
                return (obj as DynamicParameters).ParameterNames.ToList();
            }
            return GetPropertyInfos(obj).Select(x => x.Name).ToList();
        }
        private List<PropertyInfo> GetPropertyInfos(object obj)
        {
            if (obj == null)
            {
                return new List<PropertyInfo>();
            }

            List<PropertyInfo> properties;
            if (_paramCache.TryGetValue(obj.GetType(), out properties)) return properties.ToList();
            properties = obj.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public).ToList();
            _paramCache[obj.GetType()] = properties;
            return properties;
        }
    }

    #endregion

}
