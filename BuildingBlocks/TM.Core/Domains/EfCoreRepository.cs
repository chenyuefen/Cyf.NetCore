
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TM.Core.Data.EF;
using TM.Core.Data.EF.Extensions;
using TM.Core.Models;
using TM.Core.Models.Dtos;
using TM.Infrastructure.Extensions;
using TM.Infrastructure.Extensions.Common;
using Z.EntityFramework.Plus;

namespace TM.Core.Domains
{
    ///<summary>
    /// A base class for a EfCoreRepository.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity.</typeparam>
    /// <typeparam name="TKey">The type of primary key.</typeparam>
    public class EfCoreRepository<TDbContext, TEntity> : IEfCoreRepository<TDbContext, TEntity>
        where TDbContext : DbContextBase
        where TEntity : class, IDbTable, new()
    {
        private readonly TDbContext _context;
        private DbSet<TEntity> _entities;

        public EfCoreRepository(TDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 【同步】通过主键查表实体并缓存
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity QueryById(object id)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            return Entities.Find(id);
        }

        /// <summary>
        /// 【异步】通过主键查表实体并缓存
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ValueTask<TEntity> QueryByIdAsync(object id)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            return Entities.FindAsync(id);
        }

        /// <summary>
        /// 注意ef会自动把自增id返回在model
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Entities.Add(entity);
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            if (!entities.Any())
                throw new ArgumentNullException("entities");

            Entities.AddRange(entities);
        }

        [Obsolete("EFCore3.0 停止支持使用")]
        public Task InsertAsync(TEntity entity)
        {
            return InsertEntryAsync(entity).AsTask();
            //throw new NotImplementedException();
        }


        public ValueTask<EntityEntry<TEntity>> InsertEntryAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            return Entities.AddAsync(entity);

        }

        public Task InsertAsync(IEnumerable<TEntity> entities)
        {
            if (!entities.Any())
                throw new ArgumentNullException("entities");

            return Entities.AddRangeAsync(entities);
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Entities.Attach(entity);
            _context.Update(entity);
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            if (!entities.Any())
                throw new ArgumentNullException("entities");

            _context.UpdateRange(entities);

        }

        public void Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                if (string.IsNullOrEmpty(propertyName))
                {
                    propertyName = GetPropertyName(property.Body.ToString());
                }
                _context.Entry(entity).Property(propertyName).IsModified = true;

            }
        }

        string GetPropertyName(string str)
        {
            return str.Split(',')[0].Split('.')[1];
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            _context.Remove(entity);
        }


        public void Delete(IEnumerable<TEntity> entities)
        {
            if (!entities.Any())
                throw new ArgumentNullException("entities");

            _context.RemoveRange(entities);

        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            _context.RemoveRange(Entities.Where(predicate));
        }

        /// <summary>
		/// 获得列表——分页
		/// </summary>
		/// <param name="parm">PageParm</param>
		/// <returns></returns>
        public async Task<ApiResult<PagerList<TEntity>>> QueryPagesAsync(PageParm parm, Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, object>> order, bool IsAsc = true)
        {
            var res = new ApiResult<PagerList<TEntity>>();
            try
            {
                var query = Entities.Where(where);
                if (IsAsc)
                    query = query.OrderBy(order);
                else
                    query = query.OrderByDescending(order);

                res.Data = await query.ToPagedListAsync(parm.Page, parm.Limit);
                res.Message = Code.Ok.Description();
                res.Code = Code.Ok;
            }
            catch (Exception ex)
            {
                res.Message = Code.Error.Description() + ex.Message.ToHtmlSafe();
                res.Code = Code.Error;
            }
            return res;
        }

        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<TEntity> Table => Entities;

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// 使用AsNoTracking()可以提高查询效率，不用在DbContext中进行缓存,只需要读操作
        /// </summary>
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        /// <summary>
        /// Gets an entity set
        /// db.Set<User> 相当于db.User
        /// </summary>
        public virtual DbSet<TEntity> Entities => _entities ?? (_entities = _context.Set<TEntity>());

        #endregion
        public void AttachIfNot(TEntity entity)
        {
            var entry = _context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }
            //把一个没有被dbContext跟踪的对象附加到dbCotext中使其被dbContext跟踪
            _context.Attach(entity);
        }

        #region ----拓展----

        /// <summary>
        /// 读取个数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<int> QueryCountAsync(Expression<Func<TEntity, bool>> where)
        {
            return await Entities.Where(where).CountAsync();
        }

        /// <summary>
        /// 修改指定属性的单条数据
        /// </summary>
        /// <param name="entity">要修改的实体信息</param>
        /// <param name="expression">指定修改的字段</param>
        public void Update(TEntity entity, Expression<Func<TEntity, object>> expression)
        {
            _context.Update(entity);
            var entry = _context.Entry(entity);
            entry.State = EntityState.Unchanged;
            foreach (var proInfo in expression.GetPropertyAccessList())
            {
                if (!string.IsNullOrEmpty(proInfo.Name))
                    entry.Property(proInfo.Name).IsModified = true;
            }
        }

        #endregion

        #region ----cache----

        //默认缓存30天
        /// <summary>
        /// 根据条件读取缓存列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> QueryCacheListAsync(Expression<Func<TEntity, bool>> where)
        {
            return await Entities.Where(where).FromCacheAsync();
        }

        /// <summary>
        /// 读取单条缓存
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<TEntity> QueryCacheAsync(Expression<Func<TEntity, bool>> where)
        {
            var cacaheList = await Entities.Where(where).FromCacheAsync();
            return cacaheList.FirstOrDefault();
        }

        /// <summary>
        /// 读取缓存个数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<int> QueryCacheCountAsync(Expression<Func<TEntity, bool>> where)
        {
            var cacaheList = await Entities.Where(where).FromCacheAsync();
            return cacaheList.Count();
        }

        #endregion

        #region ----Plus----

        // 调用方法直接生成执行sql，不用调用SaveChanges()，因为不是由EF的上下文日志捕获执行sql
        // 注：使用cache方法不能使用下面方法，因为不经过EF的上下文处理所以不能管理cache清空      

        /// <summary>
        /// 条件批量更新
        /// </summary>
        /// <param name="where"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<bool> UpdatesAsync(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TEntity>> update)
        {
            return await Entities.Where(where).UpdateAsync(update) > 0;
        }

        /// <summary>
        /// 条件批量删除
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<bool> DeletesAsync(Expression<Func<TEntity, bool>> where)
        {
            return await Entities.Where(where).DeleteAsync() > 0;
        }

        #endregion

    }


    ///// <summary>
    ///// A base class for a repository.
    ///// </summary>
    ///// <typeparam name="TEntity">The type of entity.</typeparam>
    //public interface EfCoreRepository<TEntity> : IEfCoreRepository<TEntity, long>
    //    where TEntity : class, IEntity<long>, new()
    //{

    //}
}
