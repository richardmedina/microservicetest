using MicroserviceTest.Common.Core.Data;
using MicroserviceTest.Contract.Core.Data;
using MicroserviceTest.CoreServices.Data.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.CoreServices.Data
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }

        public async Task CreateAsync(UserData userData)
        {
            await Task.Yield();

            var userMongo = new UserMongo
            {
                UserName = userData.UserName,
                Password = userData.Password,
            };

            var collection = MongoDatabase.GetCollectionAsync<UserMongo>();
            collection.InsertOne(userMongo);
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<UserData> ReadAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UserData userData)
        {
            throw new NotImplementedException();
        }
    }
}
