using MicroserviceTest.CoreServices.Data.Attributes;
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
        private readonly MongoClient _mongoClient;

        private const string connectionUri = "mongodb://192.168.0.21:27017/?retryWrites=true&serverSelectionTimeoutMS=5000&connectTimeoutMS=10000&3t.uriVersion=3&3t.connection.name=mongoserver&3t.alwaysShowAuthDB=true&3t.alwaysShowDBFromUserRole=true";
        private const string databaseName = "microservicestest";

        public MongoDatabase()
        {
            _mongoClient = new MongoClient(connectionUri);
        }

        public IMongoCollection<TCollection> GetCollectionAsync<TCollection>()
        {
            var collectionName = typeof(TCollection).GetCustomAttribute<BsonCollectionAttribute>()?.Collection ?? typeof(TCollection).Name;

            return _mongoClient.GetDatabase(databaseName).GetCollection<TCollection>(collectionName);
        }
    }
}
