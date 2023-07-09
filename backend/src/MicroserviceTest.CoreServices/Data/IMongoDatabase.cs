using MongoDB.Driver;

namespace MicroserviceTest.CoreServices.Data
{
    public interface IMongoDatabase
    {
        IMongoCollection<TCollection> GetCollectionAsync<TCollection>();
    }
}