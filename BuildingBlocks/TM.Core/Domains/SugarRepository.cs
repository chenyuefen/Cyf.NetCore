using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TM.Core.Data.SqlSugar;
using TM.Core.Models;
using TM.Core.Models.Dtos;
using TM.Infrastructure.Extensions;
using TM.Infrastructure.Extensions.Bases;

namespace TM.Core.Domains
{
    ///<summary>
    /// A base class for a repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity.</typeparam>
    /// <typeparam name="TKey">The type of primary key.</typeparam>
    public abstract class SugarRepository<TEntity, TKey> : ISugarRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        protected string TableName { get => typeof(TEntity).Name; }

        /// <summary>
        /// Initialize the base class of a repository.
        /// </summary>
        /// <param name="dbBase">The database to access.</param>
        public SugarRepository(SugarBase dbBase)
        {
            DbBase = dbBase;
        }
        /// <summary>
        /// The database to access.
        /// </summary>
        protected SugarBase DbBase { get; }

        #region 添加操作

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="parm">TEntity</param>
        /// <returns></returns>
        public async Task<ApiResult<long>> AddAsync(TEntity parm)
        {
            var res = new ApiResult<long>() { Code = Code.Error };
            try
            {
                var dbres = await DbBase.Db.Insertable<TEntity>(parm).ExecuteCommandAsync();
                res.Data = dbres;
                res.Code = Code.Ok;
            }
            catch (Exception ex)
            {
                res.Message = Code.Error.GetEnumText() + ex.Message.ToHtmlSafe();
            }
            return res;
        }



        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="parm">List<TEntity></param>
        /// <returns></returns>
        public async Task<ApiResult<long>> AddListAsync(List<TEntity> parm)
        {
            var res = new ApiResult<long>() { Code = Code.Error };
            try
            {
                var dbres = await DbBase.Db.Insertable<TEntity>(parm).ExecuteCommandAsync();
                res.Data = dbres;
                res.Code = Code.Ok;
            }
            catch (Exception ex)
            {
                res.Message = Code.Error.GetEnumText() + ex.Message.ToHtmlSafe();

            }
            return res;
        }
        #endregion

        #region 查询操作
        /// <summary>
        /// 获得一条数据
        /// </summary>
        /// <param name="where">Expression<Func<TEntity, bool>></param>
        /// <returns></returns>
        public async Task<ApiResult<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> where)
        {
            var res = new ApiResult<TEntity>
            {
                Code = Code.Ok,
                Data = await DbBase.Db.Queryable<TEntity>().Where(where).FirstAsync() ?? new TEntity() { }
            };
            return res;
        }

        /// <summary>
        /// 获得一条数据
        /// </summary>
        /// <param name="parm">string</param>
        /// <returns></returns>
        public async Task<ApiResult<TEntity>> QueryAsync(string parm)
        {
            var res = new ApiResult<TEntity>
            {
                Code = Code.Ok,
                Data = await DbBase.Db.Queryable<TEntity>().Where(parm).FirstAsync() ?? new TEntity() { }
            };
            return res;
        }

        /// <summary>
		/// 获得列表——分页
		/// </summary>
		/// <param name="parm">PageParm</param>
		/// <returns></returns>
        public async Task<ApiResult<PagerList<TEntity>>> QueryPagesAsync(PageParm parm)
        {
            var res = new ApiResult<PagerList<TEntity>>();
            try
            {
                res.Data = await DbBase.Db.Queryable<TEntity>().ToPageAsync(parm.Page, parm.Limit);
            }
            catch (Exception ex)
            {
                res.Message = Code.Error.GetEnumText() + ex.Message.ToHtmlSafe();
                res.Code = Code.Error;
            }
            return res;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="parm">分页参数</param>
        /// <param name="where">条件</param>
        /// <param name="order">排序值</param>
        /// <param name="orderEnum">排序方式OrderByType</param>
        /// <returns></returns>
        public async Task<ApiResult<PagerList<TEntity>>> QueryPagesAsync(PageParm parm, Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, object>> order, DbOrderBy orderBy)
        {
            var res = new ApiResult<PagerList<TEntity>>();
            try
            {
                var query = DbBase.Db.Queryable<TEntity>()
                        .Where(where)
                        .OrderByIF((int)orderBy == 1, order, SqlSugar.OrderByType.Asc)
                        .OrderByIF((int)orderBy == 2, order, SqlSugar.OrderByType.Desc);
                res.Data = await query.ToPageAsync(parm.Page, parm.Limit);
            }
            catch (Exception ex)
            {
                res.Message = Code.Error.GetEnumText() + ex.Message.ToHtmlSafe();
                res.Code = Code.Error;

            }
            return res;
        }

        /// <summary>
		/// 获得列表
		/// </summary>
		/// <param name="parm">PageParm</param>
		/// <returns></returns>
        public async Task<ApiResult<List<TEntity>>> QueryListAsync(Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, object>> order, DbOrderBy orderBy)
        {
            var res = new ApiResult<List<TEntity>>();
            try
            {
                var query = DbBase.Db.Queryable<TEntity>()
                        .Where(where)
                        .OrderByIF((int)orderBy == 1, order, SqlSugar.OrderByType.Asc)
                        .OrderByIF((int)orderBy == 2, order, SqlSugar.OrderByType.Desc);
                res.Data = await query.ToListAsync();
            }
            catch (Exception ex)
            {
                res.Message = Code.Error.GetEnumText() + ex.Message.ToHtmlSafe();
                res.Code = Code.Error;

            }
            return res;
        }

        /// <summary>
        /// 获得列表，不需要任何条件
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<TEntity>>> QueryListAsync()
        {
            var res = new ApiResult<List<TEntity>>();
            try
            {
                res.Data = await DbBase.Db.Queryable<TEntity>().ToListAsync();
            }
            catch (Exception ex)
            {
                res.Message = Code.Error.GetEnumText() + ex.Message.ToHtmlSafe();
                res.Code = Code.Error;

            }
            return res;
        }
        #endregion

        #region 修改操作
        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="parm">TEntity</param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> UpdateAsync(TEntity parm)
        {
            var res = new ApiResult<bool>() { Code = Code.Error };
            try
            {
                var dbres = await DbBase.Db.Updateable<TEntity>(parm).ExecuteCommandAsync();
                res.Data = dbres > 0;
                res.Code = Code.Ok;
            }
            catch (Exception ex)
            {
                res.Message = Code.Error.GetEnumText() + ex.Message.ToHtmlSafe();

            }
            return res;
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="parm">TEntity</param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> UpdateAsync(List<TEntity> parm)
        {
            var res = new ApiResult<bool>() { Code = Code.Error };
            try
            {
                var dbres = await DbBase.Db.Updateable<TEntity>(parm).ExecuteCommandAsync();
                res.Data = dbres > 0;
                res.Code = Code.Ok;
            }
            catch (Exception ex)
            {
                res.Message = Code.Error.GetEnumText() + ex.Message.ToHtmlSafe();

            }
            return res;
        }

        /// <summary>
        /// 修改一条数据，可用作假删除
        /// </summary>
        /// <param name="columns">修改的列=Expression<Func<TEntity,TEntity>></param>
        /// <param name="where">Expression<Func<TEntity,bool>></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> UpdateAsync(Expression<Func<TEntity, TEntity>> columns,
            Expression<Func<TEntity, bool>> where)
        {
            var res = new ApiResult<bool>() { Code = Code.Error };
            try
            {
                var dbres = await DbBase.Db.Updateable<TEntity>().SetColumns(columns).Where(where).ExecuteCommandAsync();
                res.Data = dbres > 0;
                res.Code = Code.Ok;
            }
            catch (Exception ex)
            {
                res.Message = Code.Error.GetEnumText() + ex.Message;

            }
            return res;
        }
        #endregion

        #region 删除操作
        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <param name="parm">string</param>
        /// <returns></returns>
        public async Task<ApiResult<int>> DeleteAsync(string parm)
        {
            var res = new ApiResult<int>() { Code = Code.Error };
            try
            {
                var list = parm.ToListString();
                var dbres = await DbBase.Db.Deleteable<TEntity>().In(list.ToArray()).ExecuteCommandAsync();
                res.Data = dbres;
                res.Code = Code.Ok;
            }
            catch (Exception ex)
            {
                res.Message = Code.Error.GetEnumText() + ex.Message.ToHtmlSafe();

            }
            return res;
        }

        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <param name="where">Expression<Func<TEntity, bool>></param>
        /// <returns></returns>
        public async Task<ApiResult<int>> DeleteAsync(Expression<Func<TEntity, bool>> where)
        {
            var res = new ApiResult<int>() { Code = Code.Error };
            try
            {
                var dbres = await DbBase.Db.Deleteable<TEntity>().Where(where).ExecuteCommandAsync();
                res.Data = dbres;
                res.Code = Code.Ok;
            }
            catch (Exception ex)
            {
                res.Message = Code.Error.GetEnumText() + ex.Message.ToHtmlSafe();

            }
            return res;
        }
        #endregion

        #region 查询Count
        public async Task<ApiResult<long>> CountAsync(Expression<Func<TEntity, bool>> where)
        {
            var res = new ApiResult<long>() { Code = Code.Error };
            try
            {
                res.Data = await DbBase.Db.Queryable<TEntity>().CountAsync(where);
                res.Code = Code.Ok;
            }
            catch (Exception ex)
            {
                res.Message = Code.Error.GetEnumText() + ex.Message.ToHtmlSafe();

            }
            return res;
        }
        #endregion

        #region 是否存在
        public async Task<ApiResult<bool>> ExistAsync(Expression<Func<TEntity, bool>> where)
        {
            var res = new ApiResult<bool>() { Code = Code.Error };
            try
            {
                res.Data = await DbBase.Db.Queryable<TEntity>().AnyAsync(where);
                res.Code = Code.Ok;
            }
            catch (Exception ex)
            {
                res.Message = Code.Error.GetEnumText() + ex.Message.ToHtmlSafe();

            }
            return res;
        }
        #endregion
    }

    /// <summary>
    /// A base class for a repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity.</typeparam>
    public abstract class SugarRepository<TEntity> : SugarRepository<TEntity, long>
        where TEntity : class, IEntity<long>, new()
    {

        /// <summary>
        /// Initialize the base class of a repository.
        /// </summary>
        /// <param name="dbBase">The database to access.</param>
        public SugarRepository(SugarBase dbBase) : base(dbBase)
        {
        }

    }
}
