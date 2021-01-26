using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TM.Core.Models;
using TM.Core.Models.Dtos;

namespace TM.Core.Domains
{
    public interface ISugarRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        #region 添加操作

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="parm">TEntity</param>
        /// <returns></returns>
        Task<ApiResult<long>> AddAsync(TEntity parm);

        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="parm">List<TEntity></param>
        /// <returns></returns>
        Task<ApiResult<long>> AddListAsync(List<TEntity> parm);
        #endregion

        #region 查询操作
        /// <summary>
        /// 获得一条数据
        /// </summary>
        /// <param name="where">Expression<Func<TEntity, bool>></param>
        /// <returns></returns>
        Task<ApiResult<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 获得一条数据
        /// </summary>
        /// <param name="parm">string</param>
        /// <returns></returns>
        Task<ApiResult<TEntity>> QueryAsync(string parm);

        /// <summary>
		/// 获得列表——分页
		/// </summary>
		/// <param name="parm">PageParm</param>
		/// <returns></returns>
        Task<ApiResult<PagerList<TEntity>>> QueryPagesAsync(PageParm parm);

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="parm">分页参数</param>
        /// <param name="where">条件</param>
        /// <param name="order">排序值</param>
        /// <param name="orderEnum">排序方式OrderByType</param>
        /// <returns></returns>
        Task<ApiResult<PagerList<TEntity>>> QueryPagesAsync(PageParm parm, Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, object>> order, DbOrderBy orderBy);

        /// <summary>
		/// 获得列表
		/// </summary>
		/// <param name="parm">PageParm</param>
		/// <returns></returns>
        Task<ApiResult<List<TEntity>>> QueryListAsync(Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, object>> order, DbOrderBy orderBy);

        /// <summary>
        /// 获得列表，不需要任何条件
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<TEntity>>> QueryListAsync();
        #endregion

        #region 修改操作
        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="parm">TEntity</param>
        /// <returns></returns>
        Task<ApiResult<bool>> UpdateAsync(TEntity parm);

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="parm">TEntity</param>
        /// <returns></returns>
        Task<ApiResult<bool>> UpdateAsync(List<TEntity> parm);

        /// <summary>
        /// 修改一条数据，可用作假删除
        /// </summary>
        /// <param name="columns">修改的列=Expression<Func<TEntity,TEntity>></param>
        /// <param name="where">Expression<Func<TEntity,bool>></param>
        /// <returns></returns>
        Task<ApiResult<bool>> UpdateAsync(Expression<Func<TEntity, TEntity>> columns,
            Expression<Func<TEntity, bool>> where);
        #endregion

        #region 删除操作
        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <param name="parm">string</param>
        /// <returns></returns>
        Task<ApiResult<int>> DeleteAsync(string parm);

        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <param name="where">Expression<Func<TEntity, bool>></param>
        /// <returns></returns>
        Task<ApiResult<int>> DeleteAsync(Expression<Func<TEntity, bool>> where);
        #endregion

        #region 查询Count
        Task<ApiResult<long>> CountAsync(Expression<Func<TEntity, bool>> where);
        #endregion

        #region 是否存在
        Task<ApiResult<bool>> ExistAsync(Expression<Func<TEntity, bool>> where);
        #endregion
    }

    /// <summary>
    /// A base class for a repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity.</typeparam>
    public interface ISugarRepository<TEntity> : ISugarRepository<TEntity, long>
        where TEntity : class, IEntity<long>, new()
    {

    }
}
