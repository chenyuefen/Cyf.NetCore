using Dapper;
using DapperExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TM.Core.Data.EF.Abstractions;
using TM.Core.Models.Dtos;
//using ComponentAttribute = NAutowired.Core.Attributes.ComponentAttribute;

namespace TM.Core.Data.EF
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IUnitOfWork"/> 
    /// </summary>
    //[Component]
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext> where TDbContext : DbContextBase // : IUnitOfWork
    {
        //private readonly DbContext _context;
        private readonly TDbContext _dbContext;

        //private readonly ConnectionBase _dbBase;
        private bool _disposed = false;
        public string CurrentName => nameof(TDbContext);
        /// <summary>
        /// Initializes a new instance of the 
        /// </summary>
        /// <param name="context">The context.</param>
        public UnitOfWork(TDbContext dbcontext)//, ConnectionBase dbBase
        {
            _dbContext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
            //_dbBase = dbBase;
        }

        /// <summary>
        /// 实时切换数据库
        /// 缺陷就是只能同sqlser或者同mysql
        /// </summary>
        /// <param name="connectionString"></param>
        public void Initialise(string connectionString)
        {
            _dbContext.Database.GetDbConnection().ConnectionString = connectionString;
        }

        public IDbConnection DbConnection => _dbContext.Database.GetDbConnection();

        public IDbTransaction DbTransaction => _dbContext.Database.CurrentTransaction?.GetDbTransaction();

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }
        public void Complete()
        {
            if (DbTransaction != null)
                _dbContext.Database.CommitTransaction();
        }

        public void Rollback()
        {
            if (DbTransaction != null)
                _dbContext.Database.RollbackTransaction();
        }

        public IEnumerable<TEntity> Query<TEntity>(string sql, object param = null, IDbContextTransaction transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbConnection.Query<TEntity>(sql, param, transaction?.GetDbTransaction());

        }

        public async Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string sql, object param = null, IDbContextTransaction transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return await DbConnection.QueryAsync<TEntity>(sql, param, transaction?.GetDbTransaction());
        }

        public TEntity QueryFirstOrDefault<TEntity>(string sql, object param = null, IDbContextTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.QueryFirstOrDefault<TEntity>(sql, param, transaction?.GetDbTransaction(), commandTimeout, commandType);
        }

        public async Task<TEntity> QueryFirstOrDefaultAsync<TEntity>(string sql, object param = null, IDbContextTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await DbConnection.QueryFirstOrDefaultAsync<TEntity>(sql, param, transaction?.GetDbTransaction(), commandTimeout, commandType);
        }

        public int Execute(string sql, object param = null, IDbContextTransaction transaction = null, int? commandTimeout = null)
        {
            var tran = (transaction?.GetDbTransaction()) ?? DbTransaction;
            return DbConnection.Execute(sql, param, tran, commandTimeout);
        }

        public async Task<int> ExecuteAsync(string sql, object param = null, IDbContextTransaction transaction = null, int? commandTimeout = null)
        {
            var tran = (transaction?.GetDbTransaction()) ?? DbTransaction;
            return await DbConnection.ExecuteAsync(sql, param, tran, commandTimeout);
        }


        public PagerList<TEntity> QueryPagerList<TEntity>(object predicate = null, IList<ISort> sort = null, int page = 1, int resultsPerPage = 10, IDbContextTransaction transaction = null, int? commandTimeout = null) where TEntity : class
        {
            var items = DbConnection.GetPage<TEntity>(predicate, sort, page, resultsPerPage, transaction?.GetDbTransaction(), commandTimeout);
            var totalCount = DbConnection.Count<TEntity>(predicate, transaction?.GetDbTransaction(), commandTimeout);
            return new PagerList<TEntity>(page, resultsPerPage, totalCount, items.ToList());
        }

        public async Task<PagerList<TEntity>> QueryPagerListAsync<TEntity>(object predicate = null, IList<ISort> sort = null, int page = 1, int resultsPerPage = 10, IDbContextTransaction transaction = null, int? commandTimeout = null) where TEntity : class
        {
            var items = await DbConnection.GetPageAsync<TEntity>(predicate, sort, page, resultsPerPage, transaction?.GetDbTransaction(), commandTimeout);
            var totalCount = await DbConnection.CountAsync<TEntity>(predicate, transaction?.GetDbTransaction(), commandTimeout);
            return new PagerList<TEntity>(page, resultsPerPage, totalCount, items.ToList());
        }

        //public async Task<PagerList<TEntity>> QueryPagerListAsync<TEntity>(int pageIndex, int pageSize, string pageSql, object pageSqlArgs = null) where TEntity : class
        //{
        //    if (pageSize < 1 || pageSize > 5000)
        //        throw new ArgumentOutOfRangeException(nameof(pageSize));
        //    if (pageIndex < 1)
        //        throw new ArgumentOutOfRangeException(nameof(pageIndex));


        //    _dbBase.Initialise(GetConnection());



        //    //_dbBase.GetPageAsync(pageSqlArgs,)

        //    //var partedSql = PagingUtil.SplitSql(pageSql);
        //    //ISqlAdapter sqlAdapter = null;
        //    //if (_context.Database.IsMySql())
        //    //    sqlAdapter = new MysqlAdapter();
        //    //if (_context.Database.IsSqlServer())
        //    //    sqlAdapter = new SqlServerAdapter();
        //    //if (sqlAdapter == null)
        //    //    throw new Exception("Unsupported database type");
        //    //pageSql = sqlAdapter.PagingBuild(ref partedSql, pageSqlArgs, (pageIndex - 1) * pageSize, pageSize);
        //    //var sqlCount = PagingUtil.GetCountSql(partedSql);
        //    //var conn = GetConnection();
        //    //var totalCount = await conn.ExecuteScalarAsync<int>(sqlCount, pageSqlArgs);
        //    //var items = await conn.QueryAsync<TEntity>(pageSql, pageSqlArgs);
        //    //var PagerList = new PagerList<TEntity>(pageIndex - 1, pageSize, totalCount, items.ToList());
        //    //return PagerList;
        //    return null;
        //}

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public IDbConnection GetConnection()
        {
            return _dbContext.Database.GetDbConnection();
        }
    }

    public class UnitOfWork : IUnitOfWork
    {
        //private readonly DbContext _context;
        private readonly DbContext _dbContext;

        //private readonly ConnectionBase _dbBase;
        private bool _disposed = false;
        public string CurrentName => nameof(DbContext);
        /// <summary>
        /// Initializes a new instance of the 
        /// </summary>
        /// <param name="context">The context.</param>
        public UnitOfWork(DbContext dbcontext)//, ConnectionBase dbBase
        {
            _dbContext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
            //_dbBase = dbBase;
        }

        public IDbConnection DbConnection => _dbContext.Database.GetDbConnection();

        public IDbTransaction DbTransaction => _dbContext.Database.CurrentTransaction?.GetDbTransaction();
        /// <summary>
        /// 实时切换数据库
        /// 缺陷就是只能同sqlser或者同mysql
        /// </summary>
        /// <param name="connectionString"></param>
        public void Initialise(string connectionString)
        {
            _dbContext.Database.GetDbConnection().ConnectionString = connectionString;
        }
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }
        public void Complete()
        {
            if (DbTransaction != null)
                _dbContext.Database.CommitTransaction();
        }

        public void Rollback()
        {
            if (DbTransaction != null)
                _dbContext.Database.RollbackTransaction();
        }

        public IEnumerable<TEntity> Query<TEntity>(string sql, object param = null, IDbContextTransaction transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbConnection.Query<TEntity>(sql, param, transaction?.GetDbTransaction());
        }

        public async Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string sql, object param = null, IDbContextTransaction transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return await DbConnection.QueryAsync<TEntity>(sql, param, transaction?.GetDbTransaction());
        }

        public TEntity QueryFirstOrDefault<TEntity>(string sql, object param = null, IDbContextTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return DbConnection.QueryFirstOrDefault<TEntity>(sql, param, transaction?.GetDbTransaction(), commandTimeout, commandType);
        }

        public async Task<TEntity> QueryFirstOrDefaultAsync<TEntity>(string sql, object param = null, IDbContextTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await DbConnection.QueryFirstOrDefaultAsync<TEntity>(sql, param, transaction?.GetDbTransaction(), commandTimeout, commandType);
        }

        public int Execute(string sql, object param, IDbContextTransaction transaction = null, int? commandTimeout = null)
        {
            var tran = (transaction?.GetDbTransaction()) ?? DbTransaction;
            return DbConnection.Execute(sql, param, tran, commandTimeout);
        }
        public async Task<int> ExecuteAsync(string sql, object param, IDbContextTransaction transaction = null, int? commandTimeout = null)
        {
            var tran = (transaction?.GetDbTransaction()) ?? DbTransaction;
            return await DbConnection.ExecuteAsync(sql, param, tran, commandTimeout);
        }

        public PagerList<TEntity> QueryPagerList<TEntity>(object predicate = null, IList<ISort> sort = null, int page = 1, int resultsPerPage = 10, IDbContextTransaction transaction = null, int? commandTimeout = null) where TEntity : class
        {
            var items = DbConnection.GetPage<TEntity>(predicate, sort, page, resultsPerPage, transaction?.GetDbTransaction(), commandTimeout);
            var totalCount = DbConnection.Count<TEntity>(predicate, transaction?.GetDbTransaction(), commandTimeout);
            return new PagerList<TEntity>(page, resultsPerPage, totalCount, items.ToList());
        }

        public async Task<PagerList<TEntity>> QueryPagerListAsync<TEntity>(object predicate = null, IList<ISort> sort = null, int page = 1, int resultsPerPage = 10, IDbContextTransaction transaction = null, int? commandTimeout = null) where TEntity : class
        {
            var items = await DbConnection.GetPageAsync<TEntity>(predicate, sort, page, resultsPerPage, transaction?.GetDbTransaction(), commandTimeout);
            var totalCount = await DbConnection.CountAsync<TEntity>(predicate, transaction?.GetDbTransaction(), commandTimeout);
            return new PagerList<TEntity>(page, resultsPerPage, totalCount, items.ToList());
        }

        //public async Task<PagerList<TEntity>> QueryPagerListAsync<TEntity>(int pageIndex, int pageSize, string pageSql, object pageSqlArgs = null) where TEntity : class
        //{
        //    if (pageSize < 1 || pageSize > 5000)
        //        throw new ArgumentOutOfRangeException(nameof(pageSize));
        //    if (pageIndex < 1)
        //        throw new ArgumentOutOfRangeException(nameof(pageIndex));


        //    _dbBase.Initialise(GetConnection());



        //    //_dbBase.GetPageAsync(pageSqlArgs,)

        //    //var partedSql = PagingUtil.SplitSql(pageSql);
        //    //ISqlAdapter sqlAdapter = null;
        //    //if (_context.Database.IsMySql())
        //    //    sqlAdapter = new MysqlAdapter();
        //    //if (_context.Database.IsSqlServer())
        //    //    sqlAdapter = new SqlServerAdapter();
        //    //if (sqlAdapter == null)
        //    //    throw new Exception("Unsupported database type");
        //    //pageSql = sqlAdapter.PagingBuild(ref partedSql, pageSqlArgs, (pageIndex - 1) * pageSize, pageSize);
        //    //var sqlCount = PagingUtil.GetCountSql(partedSql);
        //    //var conn = GetConnection();
        //    //var totalCount = await conn.ExecuteScalarAsync<int>(sqlCount, pageSqlArgs);
        //    //var items = await conn.QueryAsync<TEntity>(pageSql, pageSqlArgs);
        //    //var PagerList = new PagerList<TEntity>(pageIndex - 1, pageSize, totalCount, items.ToList());
        //    //return PagerList;
        //    return null;
        //}

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public IDbConnection GetConnection()
        {
            return _dbContext.Database.GetDbConnection();
        }
    }
}
