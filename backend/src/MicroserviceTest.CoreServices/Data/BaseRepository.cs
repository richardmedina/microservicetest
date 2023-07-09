using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.CoreServices.Data
{
    public abstract class BaseRepository 
    {
        protected readonly IMongoDatabase MongoDatabase;

        public BaseRepository(IMongoDatabase mongoDatabase)
        {
            MongoDatabase = mongoDatabase;
        }
    }

    public abstract class BaseRepository<TCollection> : BaseRepository
    {
        protected readonly IMongoCollection<TCollection>? Collection;

        public BaseRepository(IMongoDatabase mongoDatabase) : base (mongoDatabase)
        {
            Collection = MongoDatabase.GetCollectionAsync<TCollection>().Result;
        }
    }
}
