using DapperExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TM.Core.Data.EF;
using TM.Core.Models;

namespace TM.Core.Domains
{
    public class DapperRepository<TDbContext, TEntity> : IDapperRepository<TDbContext, TEntity>
         where TDbContext : DbContextBase
         where TEntity : class, IDbTable, new()
    {
        private readonly TDbContext _context;

        public DapperRepository(TDbContext context)
        {
            _context = context;
        }

        public IDbConnection DbConnection => _context.Database.GetDbConnection();

        public IDbTransaction DbTransaction => _context.Database.CurrentTransaction?.GetDbTransaction();

        public void Insert(TEntity entity)
        {
            DbConnection.Insert(entity, DbTransaction);
        }
        public async Task InsertAsync(TEntity entity)
        {
            await DbConnection.InsertAsync(entity);
        }


        public void Insert(IEnumerable<TEntity> entities)
        {
            DbConnection.Insert(entities, DbTransaction);
        }

        public async Task InsertAsync(IEnumerable<TEntity> entities)
        {
            await DbConnection.InsertAsync(entities, DbTransaction);
        }



        public TEntity QueryById(object id)
        {
            return DbConnection.Get<TEntity>(id);
        }

        public async ValueTask<TEntity> QueryByIdAsync(object id)
        {
            return await DbConnection.GetAsync<TEntity>(id);
        }

        public void Update(TEntity entity)
        {
            DbConnection.Update(entity);
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            DbConnection.Update(entities);
        }

        public void Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            DbConnection.Delete(entity, DbTransaction);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            DbConnection.Delete(entities, DbTransaction);
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            DbConnection.Delete(predicate, DbTransaction);
        }


    }
}
