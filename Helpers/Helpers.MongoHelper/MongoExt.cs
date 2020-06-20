using Helpers.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Driver;
using Polly;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helpers
{
    public static class MongoExt
    {
        public static IServiceCollection AddMongoExt(this IServiceCollection services, IConfiguration configuration)
        {
            BsonSerializer.RegisterSerializer<DateTimeOffset>(new DateTimeOffsetSupportingBsonDateTimeSerializer());
            ConventionRegistry.Register("DictionaryRepresentationConvention", new ConventionPack { new DictionaryRepresentationConvention(DictionaryRepresentation.ArrayOfArrays) }, _ => true);

            services.AddTransient<IMongoRepository, MongoRepository>();
            services.AddTransient<IMongoRepositoryAsync, MongoRepositoryAsync>();

            services.Configure<MongoConfig>(configuration.GetSection("mongo"));
            //services.Configure<MongoConfigurationOptions>(configuration);
            services.AddSingleton<IMongoClient>(x => new MongoClient(x.GetRequiredService<IOptions<MongoConfig>>().Value.Connection));
            services.AddSingleton<IMongoDatabase>(x => x.GetRequiredService<IMongoClient>().GetDatabase(x.GetRequiredService<IOptions<MongoConfig>>().Value.DatabaseName));
            return services;
        }

        public static IMongoCollection<T> GetCollection<T>(this IMongoDatabase mongoDatabase, MongoCollectionSettings settings = null)
        {
            return mongoDatabase.GetCollection<T>(typeof(T).Name, settings);
        }

        public static T Deserialize<T>(this BsonDocument document)
        {
            return BsonSerializer.Deserialize<T>(document);
        }

        public static List<T> DeserializeList<T>(this List<BsonDocument> documents)
        {
            return documents?.Select(x => x.Deserialize<T>()).ToList();
        }
    }
}
