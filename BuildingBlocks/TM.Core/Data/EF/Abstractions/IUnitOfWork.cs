using DapperExtensions;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TM.Core.Models.Dtos;

namespace TM.Core.Data.EF.Abstractions
{
    /// <summary>
    /// Defines the interface(s) for unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Initialise or set connectionString here in your need.
        /// </summary>
        /// <param name="connectionString"></param>
        void Initialise(string connectionString);

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        int SaveChanges();

        /// <summary>
        /// Asynchronously saves all changes made in this unit of work to the database.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous save operation. The task result contains the number of state entities written to database.</returns>
        Task<int> SaveChangesAsync();

        #region command sql

        /// <summary>
        /// QueryAsync
        /// ag:await _unitOfWork.QueryAsync`Demo`("select id,name from school where id = @id", new { id = 1 });
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Query<TEntity>(string sql, object param = null, IDbContextTransaction transaction = null, int? commandTimeout = null) where TEntity : class;


        /// <summary>
        /// QueryAsync
        /// ag:await _unitOfWork.QueryAsync`Demo`("select id,name from school where id = @id", new { id = 1 });
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string sql, object param = null, IDbContextTransaction transaction = null, int? commandTimeout = null) where TEntity : class;

        /// <summary>
        /// QueryFirstOrDefault
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        TEntity QueryFirstOrDefault<TEntity>(string sql, object param = null, IDbContextTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// QueryFirstOrDefaultAsync
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<TEntity> QueryFirstOrDefaultAsync<TEntity>(string sql, object param = null, IDbContextTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// ExecuteAsync
        /// ag:await _unitOfWork.ExecuteAsync("update school set name =@name where id =@id", new { name = "", id=1 });
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        int Execute(string sql, object param, IDbContextTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// ExecuteAsync
        /// ag:await _unitOfWork.ExecuteAsync("update school set name =@name where id =@id", new { name = "", id=1 });
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        Task<int> ExecuteAsync(string sql, object param = null, IDbContextTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// QueryPagedList, complex sql, use "select * from (your sql) b"
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageSql"></param>
        /// <param name="pageSqlArgs"></param>
        /// <returns></returns>
        //Task<PagerList<TEntity>> QueryPagedListAsync<TEntity>(int pageIndex, int pageSize, string pageSql, object pageSqlArgs = null)
        //    where TEntity : class;
        PagerList<TEntity> QueryPagerList<TEntity>(object predicate = null, IList<ISort> sort = null, int page = 1, int resultsPerPage = 10, IDbContextTransaction transaction = null, int? commandTimeout = null)
              where TEntity : class;


        /// <summary>
        /// QueryPagedListAsync, complex sql, use "select * from (your sql) b"
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageSql"></param>
        /// <param name="pageSqlArgs"></param>
        /// <returns></returns>
        //Task<PagerList<TEntity>> QueryPagedListAsync<TEntity>(int pageIndex, int pageSize, string pageSql, object pageSqlArgs = null)
        //    where TEntity : class;
        Task<PagerList<TEntity>> QueryPagerListAsync<TEntity>(object predicate = null, IList<ISort> sort = null, int page = 1, int resultsPerPage = 10, IDbContextTransaction transaction = null, int? commandTimeout = null)
              where TEntity : class;

        #endregion

        #region Transaction
        /// <summary>
        /// BeginTransaction
        /// </summary>
        /// <returns></returns>
        IDbContextTransaction BeginTransaction();

        /// <summary>
        /// BeginTransactionAsync
        /// </summary>
        /// <returns></returns>
        Task<IDbContextTransaction> BeginTransactionAsync();

        /// <summary>
        /// when using EFCore BeginTransaction,you need Savechanges and Complete them all
        /// </summary>
        void Complete();

        /// <summary>
        /// Rollback
        /// </summary>
        void Rollback();


        #endregion
        /// <summary>
        /// get connection
        /// </summary>
        /// <returns></returns>
        IDbConnection GetConnection();

        IDbConnection DbConnection { get; }

        IDbTransaction DbTransaction { get; }
    }

    public interface IUnitOfWork<TDbContext> : IUnitOfWork, IDisposable where TDbContext : DbContextBase
    {

    }
}
