using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Microsoft.AspNetCore.Identity;

using Todo.Service.Interfaces;
using Todo.Service.Config;

namespace Todo.Service.MongoDB;
public static class Extensions
{
    public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration config)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

        services.AddSingleton(_ =>
        {
            var dbCongig = config.GetSection(nameof(MongoConfig)).Get<MongoConfig>();
            var mongoClient = new MongoClient(dbCongig.ConnectionString);
            return mongoClient.GetDatabase(dbCongig.Name);
        });
        return services;
    }

    public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collectionName) where T : IEntity
    {
        services.AddSingleton<IRepository<T>>(s =>
        {
            var dbMongo = s.GetService<IMongoDatabase>();
            return new MongoRepository<T>(dbMongo, collectionName);
        });

        return services;
    }
}
