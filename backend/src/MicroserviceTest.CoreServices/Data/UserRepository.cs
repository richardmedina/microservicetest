using MicroserviceTest.Common.Core.Data;
using MicroserviceTest.Contract.Core.Data;
using MicroserviceTest.CoreServices.Data.Collections;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.CoreServices.Data
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private IMongoCollection<UserMongo> userCollection;
        public UserRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
            userCollection = MongoDatabase.GetCollectionAsync<UserMongo>().Result;
        }

        public async Task CreateAsync(UserData userData)
        {
            await Task.Yield();

            var userMongo = new UserMongo
            {
                UserName = userData.UserName,
                Password = userData.Password,
            };

            userCollection.InsertOne(userMongo);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var filter = new FilterDefinitionBuilder<UserMongo>().Eq(user => user.Id, id);
            await userCollection.DeleteOneAsync(filter);

            return true;
        }

        public async Task<IEnumerable<UserData>> ReadAsync()
        {
            var filter = new FilterDefinitionBuilder<UserMongo>().Empty;
            
            var cursor = await userCollection.FindAsync(filter);

            return cursor.ToList().Select(user => new UserData
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password
            });
        }

        public async Task<UserData?> ReadAsync(string id)
        {
            var filter = new FilterDefinitionBuilder<UserMongo>().Eq(user => user.Id, id);

            var cursor = await userCollection.FindAsync(filter);
            var user = cursor.FirstOrDefault();

            return user != null
                ? new UserData
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Password = user.Password
                }
                : null;
        }

        public Task<bool> UpdateAsync(UserData userData)
        {
            throw new NotImplementedException();
        }
    }
}
