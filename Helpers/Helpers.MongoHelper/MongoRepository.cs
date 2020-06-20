using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Helpers
{
    /// <summary>
    /// mongo仓储
    /// </summary>
    public class MongoRepository : IMongoRepository
    {
        protected static IMongoDatabase _database;
        public MongoRepository(IMongoDatabase mongoDatabase)
        {
            _database = mongoDatabase;
        }

        public IQueryable<T> All<T>() where T : class, new()
        {
            return _database.GetCollection<T>(typeof(T).Name).AsQueryable();
        }

        public IQueryable<T> Where<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, new()
        {
            return All<T>().Where(expression);
        }

        public void Delete<T>(System.Linq.Expressions.Expression<Func<T, bool>> predicate) where T : class, new()
        {
            var result = _database.GetCollection<T>(typeof(T).Name).DeleteMany(predicate);

        }
        public T Single<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, new()
        {
            return All<T>().Where(expression).SingleOrDefault();
        }

        public bool CollectionExists<T>() where T : class, new()
        {
            var collection = _database.GetCollection<T>(typeof(T).Name);
            var filter = new BsonDocument();
            var totalCount = collection.CountDocuments(filter);
            return (totalCount > 0) ? true : false;

        }

        public void Add<T>(T item) where T : class, new()
        {
            _database.GetCollection<T>(typeof(T).Name).InsertOne(item);
        }

        public void Add<T>(IEnumerable<T> items) where T : class, new()
        {
            _database.GetCollection<T>(typeof(T).Name).InsertMany(items);
        }
    }

    public class MongoRepositoryAsync : MongoRepository,IMongoRepositoryAsync
    {

        public MongoRepositoryAsync(IMongoDatabase mongoDatabase):base(mongoDatabase)
        {
        }

        public Task AddAsync<T>(T item) where T : class, new()
        {
            return _database.GetCollection<T>(typeof(T).Name).InsertOneAsync(item);
        }

        public Task AddAsync<T>(IEnumerable<T> items) where T : class, new()
        {
            return _database.GetCollection<T>(typeof(T).Name).InsertManyAsync(items);
        }


        public async Task<bool> CollectionExistsAsync<T>() where T : class, new()
        {
            var filter = new BsonDocument();
            var totalCount = await _database.GetCollection<T>(typeof(T).Name).CountDocumentsAsync(filter);
            return (totalCount > 0) ? true : false;
        }

        public Task DeleteAsync<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            return _database.GetCollection<T>(typeof(T).Name).DeleteManyAsync(expression);
        }

        public Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            return _database.GetCollection<T>(typeof(T).Name).Find(expression).SingleOrDefaultAsync();
        }
    }
}
