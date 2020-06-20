using Helpers.Mongo;
using Helpers.WebHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helpers
{
    public static class MongoExt
    {
        ///// <summary>
        ///// 注册mongo
        ///// </summary>
        ///// <param name="services"></param>
        ///// <param name="configuration"></param>
        ///// <returns></returns>
        //public static IServiceCollection AddMongoExt(this IServiceCollection services, IConfiguration configuration)
        //{
        //    BsonSerializer.RegisterSerializer<DateTimeOffset>(new DateTimeOffsetSupportingBsonDateTimeSerializer());
        //    ConventionRegistry.Register("DictionaryRepresentationConvention", new ConventionPack { new DictionaryRepresentationConvention(DictionaryRepresentation.ArrayOfArrays) }, _ => true);
        //    services.Configure<MongoConfigurationOptions>(configuration);
        //    services.AddTransient<IMongoRepository, MongoRepository>();
        //    services.AddTransient<IMongoRepositoryAsync, MongoRepositoryAsync>();
        //    services.AddSingleton<IMongoClient>(x => new MongoClient(x.GetRequiredService<IOptions<MongoConfigurationOptions>>().Value.MongoConnection));
        //    services.AddSingleton<IMongoDatabase>(x => x.GetRequiredService<IMongoClient>().GetDatabase(x.GetRequiredService<IOptions<MongoConfigurationOptions>>().Value.MongoDatabaseName));
        //    return services;
        //}

        //public static IMongoCollection<T> GetCollection<T>(this IMongoDatabase mongoDatabase, MongoCollectionSettings settings = null)
        //{
        //    return mongoDatabase.GetCollection<T>(typeof(T).Name, settings);
        //}
    }
}
