using MicroserviceTest.CoreServices.Data.Attributes;
using MicroserviceTest.CoreServices.Data.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.CoreServices.Data
{
    public class MongoDatabase : IMongoDatabase
    {
        private readonly MongoDbOptions _options;
        private readonly MongoClient _mongoClient;

        // Demo mongo environment
        // private const string connectionUri = "mongodb://192.168.0.21:27017/?retryWrites=true&serverSelectionTimeoutMS=5000&connectTimeoutMS=10000&3t.uriVersion=3&3t.connection.name=mongoserver&3t.alwaysShowAuthDB=true&3t.alwaysShowDBFromUserRole=true";
        // private const string databaseName = "microservicestest";

        public MongoDatabase(IOptions<MongoDbOptions> mongoDbOptions)
        {
            _options = mongoDbOptions.Value;
            _mongoClient = new MongoClient(_options.ConnectionString);
        }

        public async Task<IMongoCollection<TCollection>> GetCollectionAsync<TCollection>()
        {
            await Task.CompletedTask;
            var collectionName = typeof(TCollection).GetCustomAttribute<BsonCollectionAttribute>()?.Collection ?? typeof(TCollection).Name;

            return _mongoClient.GetDatabase(_options.Database).GetCollection<TCollection>(collectionName);
        }
    }
}
