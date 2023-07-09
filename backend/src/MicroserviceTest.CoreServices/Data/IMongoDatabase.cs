using MongoDB.Driver;

namespace MicroserviceTest.CoreServices.Data
{
    public interface IMongoDatabase
    {
        Task<IMongoCollection<TCollection>> GetCollectionAsync<TCollection>();
    }
}