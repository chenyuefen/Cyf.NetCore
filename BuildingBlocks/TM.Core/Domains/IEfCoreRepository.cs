using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TM.Core.Data.EF;
using TM.Core.Models;
using TM.Core.Models.Dtos;

namespace TM.Core.Domains
{
    /// <summary>
    /// This interface is implemented by EFCore repositories to ensure implementation of fixed methods.
    /// </summary>
    /// <typeparam name="TEntity">Main Entity type this repository works on</typeparam>
    public interface IEfCoreRepository<TDbContext, TEntity> : IBasicRepository<TDbContext, TEntity>
        where TDbContext : DbContextBase
        where TEntity : class, IDbTable, new()
    {

        /// <summary>
        /// EFCore3.0兼容
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ValueTask<EntityEntry<TEntity>> InsertEntryAsync(TEntity entity);

        #region Select/Get/Query

        /// <summary>
        /// Table Tracking
        /// </summary>
        IQueryable<TEntity> Table { get; }

        /// <summary>
        /// TableNoTracking
        /// </summary>
        IQueryable<TEntity> TableNoTracking { get; }

        #endregion

        DbSet<TEntity> Entities { get; }
        void AttachIfNot(TEntity entity);

        Task<ApiResult<PagerList<TEntity>>> QueryPagesAsync(PageParm parm, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> order, bool IsAsc = true);

        #region ----拓展----

        /// <summary>
        /// 读取条数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<int> QueryCountAsync(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 修改指定属性的单条数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="expression"></param>
        void Update(TEntity model, Expression<Func<TEntity, object>> expression);

        #endregion

        #region ----Cache----

        /// <summary>
        /// 读取缓存列表数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> QueryCacheListAsync(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 读取缓存数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<TEntity> QueryCacheAsync(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 读取缓存个数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<int> QueryCacheCountAsync(Expression<Func<TEntity, bool>> where);

        #endregion

        #region ----Plus----

        /// <summary>
        /// 条件批量编辑
        /// </summary>
        /// <param name="where"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        Task<bool> UpdatesAsync(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TEntity>> update);

        /// <summary>
        /// 条件批量删除
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<bool> DeletesAsync(Expression<Func<TEntity, bool>> where);

        #endregion

    }

}
