using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Helpers
{
    public interface IMongoRepository
    {
        System.Linq.IQueryable<T> All<T>() where T : class, new();
        IQueryable<T> Where<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, new();
        T Single<T>(Expression<Func<T, bool>> expression) where T : class, new();
        void Delete<T>(Expression<Func<T, bool>> expression) where T : class, new();
        void Add<T>(T item) where T : class, new();
        void Add<T>(IEnumerable<T> items) where T : class, new();
        bool CollectionExists<T>() where T : class, new();
    }

    public interface IMongoRepositoryAsync : IMongoRepository
    {
        Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class, new();
        Task DeleteAsync<T>(Expression<Func<T, bool>> expression) where T : class, new();
        Task AddAsync<T>(T item) where T : class, new();
        Task AddAsync<T>(IEnumerable<T> items) where T : class, new();
        Task<bool> CollectionExistsAsync<T>() where T : class, new();
    }
}
