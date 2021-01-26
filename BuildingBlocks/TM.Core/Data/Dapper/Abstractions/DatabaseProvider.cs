using System;
using System.Data.Common;

namespace TM.Core.Data.Dapper
{
    internal static class Singleton<T> where T : new()
    {
        public static T Instance = new T();
    }
    /// <summary>
    ///     Base class for DatabaseType handlers - provides default/common handling for different database engines
    /// </summary>
    public abstract class DatabaseProvider : IDbProvider
    {
        /// <summary>
        ///     Gets the DbProviderFactory for this database provider.
        /// </summary>
        /// <returns>The provider factory.</returns>
        public abstract DbProviderFactory GetFactory();

        /// <summary>
        ///     Returns the .net standard conforming DbProviderFactory.
        /// </summary>
        /// <param name="assemblyQualifiedNames">The assembly qualified name of the provider factory.</param>
        /// <returns>The db provider factory.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="assemblyQualifiedNames" /> does not match a type.</exception>
        protected DbProviderFactory GetFactory(params string[] assemblyQualifiedNames)
        {
            Type ft = null;
            foreach (var assemblyName in assemblyQualifiedNames)
            {
                ft = Type.GetType(assemblyName);

                if (ft != null)
                {
                    break;
                }
            }

            if (ft == null)
                throw new ArgumentException("Could not load the " + GetType().Name + " DbProviderFactory.");

            return (DbProviderFactory)ft.GetField("Instance").GetValue(null);
        }

        /// <summary>
        ///     Look at the type and provider name being used and instantiate a suitable DatabaseType instance.
        /// </summary>
        /// <param name="type">The type name.</param>
        /// <param name="allowDefault">A flag that when set allows the default <see cref="SqlServerDatabaseProvider"/> to be returned if not match is found.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>The database provider.</returns>
        internal static IDbProvider Resolve(Type type, bool allowDefault, string connectionString)
        {
            var typeName = type.Name;
            if (typeName.Equals("SqlConnection") || typeName.Equals("SqlClientFactory"))
                return Singleton<SqlServerDatabaseProvider>.Instance;
            // Try using type name first (more reliable)
            if (typeName.StartsWith("MySql"))
                return Singleton<MySqlDatabaseProvider>.Instance;
            //if (typeName.StartsWith("MariaDb"))
            //    return Singleton<MariaDbDatabaseProvider>.Instance;
            //if (typeName.StartsWith("SqlCe"))
            //    return Singleton<SqlServerCEDatabaseProviders>.Instance;
            //if (typeName.StartsWith("Npgsql") || typeName.StartsWith("PgSql"))
            //    return Singleton<PostgreSQLDatabaseProvider>.Instance;
            if (typeName.StartsWith("Oracle"))
                return Singleton<OracleDatabaseProvider>.Instance;
            //if (typeName.StartsWith("SQLite"))
            //    return Singleton<SQLiteDatabaseProvider>.Instance;
            //if (typeName.StartsWith("FbConnection") || typeName.EndsWith("FirebirdClientFactory"))
            //    return Singleton<FirebirdDbDatabaseProvider>.Instance;
            //if (typeName.IndexOf("OleDb", StringComparison.InvariantCultureIgnoreCase) >= 0
            //    && (connectionString.IndexOf("Jet.OLEDB", StringComparison.InvariantCultureIgnoreCase) > 0 || connectionString.IndexOf("ACE.OLEDB", StringComparison.InvariantCultureIgnoreCase) > 0))
            //{
            //    return Singleton<MsAccessDbDatabaseProvider>.Instance;
            //}
            if (!allowDefault)
                throw new ArgumentException("Could not match `" + type.FullName + "` to a provider.", "type");

            // Assume SQL Server
            return Singleton<SqlServerDatabaseProvider>.Instance;
        }

        /// <summary>
        ///     Look at the type and provider name being used and instantiate a suitable DatabaseType instance.
        /// </summary>
        /// <param name="providerName">The provider name.</param>
        /// <param name="allowDefault">A flag that when set allows the default <see cref="SqlServerDatabaseProvider"/> to be returned if not match is found.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>The database type.</returns>
        internal static IDbProvider Resolve(string providerName, bool allowDefault, string connectionString)
        {
            //// Try again with provider name
            if (providerName.IndexOf("MySql", StringComparison.InvariantCultureIgnoreCase) >= 0)
                return Singleton<MySqlDatabaseProvider>.Instance;
            //if (providerName.IndexOf("MariaDb", StringComparison.InvariantCultureIgnoreCase) >= 0)
            //    return Singleton<MariaDbDatabaseProvider>.Instance;
            //if (providerName.IndexOf("SqlServerCe", StringComparison.InvariantCultureIgnoreCase) >= 0 ||
            //    providerName.IndexOf("SqlCeConnection", StringComparison.InvariantCultureIgnoreCase) >= 0)
            //    return Singleton<SqlServerCEDatabaseProviders>.Instance;
            if (providerName.IndexOf("Npgsql", StringComparison.InvariantCultureIgnoreCase) >= 0
                || providerName.IndexOf("pgsql", StringComparison.InvariantCultureIgnoreCase) >= 0)
                return Singleton<PostgreSQLDatabaseProvider>.Instance;
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
            if (providerName.IndexOf("SqlServer", StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                providerName.IndexOf("System.Data.SqlClient", StringComparison.InvariantCultureIgnoreCase) >= 0)
                return Singleton<SqlServerDatabaseProvider>.Instance;

            if (!allowDefault)
                throw new ArgumentException("Could not match `" + providerName + "` to a provider.", "providerName");

            // Assume SQL Server
            return Singleton<SqlServerDatabaseProvider>.Instance;
        }

        /// <summary>
        ///     Unwraps a wrapped <see cref="DbProviderFactory"/>.
        /// </summary>
        /// <param name="factory">The factory to unwrap.</param>
        /// <returns>The unwrapped factory or the original factory if no wrapping occurred.</returns>
        internal static DbProviderFactory Unwrap(DbProviderFactory factory)
        {
            var sp = factory as IServiceProvider;

            if (sp == null)
                return factory;

            var unwrapped = sp.GetService(factory.GetType()) as DbProviderFactory;
            return unwrapped == null ? factory : Unwrap(unwrapped);
        }

        /// <summary>
        ///     Escape and arbitary SQL identifier into a format suitable for the associated database provider
        /// </summary>
        /// <param name="sqlIdentifier">The SQL identifier to be escaped</param>
        /// <returns>The escaped identifier</returns>
        public virtual string EscapeSqlIdentifier(string sqlIdentifier)
        {
            return string.Format("[{0}]", sqlIdentifier);
        }

        /// <summary>
        ///     Returns the prefix used to delimit parameters in SQL query strings.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>The providers character for prefixing a query parameter.</returns>
        public virtual string GetParameterPrefix(string connectionString)
        {
            return "@";
        }
    }

    public class SqlServerDatabaseProvider : DatabaseProvider
    {
        public override DbProviderFactory GetFactory()
        {
            return GetFactory("System.Data.SqlClient.SqlClientFactory, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
        }
    }
    public class MySqlDatabaseProvider : DatabaseProvider
    {
        public override DbProviderFactory GetFactory()
        {
            return GetFactory("MySql.Data.MySqlClient.MySqlClientFactory, MySql.Data, Culture=neutral, PublicKeyToken=c5687fc88969c44d");
        }

        public override string GetParameterPrefix(string connectionString)
        {
            if (connectionString != null && connectionString.IndexOf("Allow User Variables=true") >= 0)
                return "?";
            else
                return "@";
        }

        public override string EscapeSqlIdentifier(string sqlIdentifier)
        {
            return string.Format("`{0}`", sqlIdentifier);
        }


    }
    public class OracleDatabaseProvider : DatabaseProvider
    {
        public override string GetParameterPrefix(string connectionString)
        {
            return ":";
        }

        public override DbProviderFactory GetFactory()
        {
            // "Oracle.ManagedDataAccess.Client.OracleClientFactory, Oracle.ManagedDataAccess" is for Oracle.ManagedDataAccess.dll
            // "Oracle.DataAccess.Client.OracleClientFactory, Oracle.DataAccess" is for Oracle.DataAccess.dll
            return GetFactory("Oracle.ManagedDataAccess.Client.OracleClientFactory, Oracle.ManagedDataAccess, Culture=neutral, PublicKeyToken=89b483f429c47342",
                              "Oracle.DataAccess.Client.OracleClientFactory, Oracle.DataAccess");
        }

        public override string EscapeSqlIdentifier(string sqlIdentifier)
        {
            return string.Format("\"{0}\"", sqlIdentifier.ToUpperInvariant());
        }

    }
    public class PostgreSQLDatabaseProvider : DatabaseProvider
    {
        public override DbProviderFactory GetFactory()
        {
            return GetFactory("Npgsql.NpgsqlFactory, Npgsql, Culture=neutral, PublicKeyToken=5d8b90d52f46fda7");
        }
        public override string EscapeSqlIdentifier(string sqlIdentifier)
        {
            return string.Format("\"{0}\"", sqlIdentifier);
        }
    }
}
