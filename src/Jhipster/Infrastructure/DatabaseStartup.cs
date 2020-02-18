using MyCompany.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using System.Reflection;
using System.Security.Authentication;
using MongoDB.Bson.Serialization.IdGenerators;

namespace MyCompany.Infrastructure {
    public static class DatabaseConfiguration {
        public static IServiceCollection AddDatabaseModule(this IServiceCollection @this, IConfiguration configuration)
        {
            RegisterBsonMaps();
            var settings = MongoClientSettings.FromUrl(new MongoUrl("mongodb://localhost:27017"));
            settings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };
            var client = new MongoClient(settings);
            var database = client.GetDatabase("TestMongoDB");
            @this.AddSingleton(database);
            @this.AddScoped(typeof(MongoDataContext<>));

            return @this;
        }

        private static void RegisterBsonMaps()
        {
            foreach (var type in MongoCollections.TypeCollectionMap.Keys)
            {
                if (!BsonClassMap.IsClassMapRegistered(type))
                {
                    typeof(DatabaseConfiguration)
                        .GetMethod("Init", BindingFlags.Static | BindingFlags.NonPublic)?
                        .MakeGenericMethod(type)
                        .Invoke(null, null);
                }
            }
        }

        private static void Initializer<TClass>(BsonClassMap<TClass> cm)
            where TClass : IModel
        {
            cm.AutoMap();
            cm.MapIdMember(c => c.Id).SetIdGenerator(CombGuidGenerator.Instance);
            cm.SetDiscriminator(typeof(TClass).Name);
            cm.SetDiscriminatorIsRequired(true);
            cm.SetIgnoreExtraElements(true);
        }

        private static void Init<TClass>()
            where TClass : IModel
        {
            BsonClassMap.RegisterClassMap<TClass>(Initializer);
        }
    }
}
