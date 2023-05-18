using MicroserviceTest.Common.Services;
using MicroserviceTest.Contract.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.Services.User
{
    public class UserService : IUserService
    {
        private readonly List<UserDto> users = new List<UserDto>();

        private long GetNextId() => users.Count == 0 ? 1 : users.Max(u => u.Id) + 1;

        public async Task<bool> CreateAsync(CreateUserDto createUserDto)
        {
            await Task.CompletedTask;

            var user = new UserDto(GetNextId(), createUserDto.UserName, createUserDto.FirstName, createUserDto.LastName);

            users.Add(user);

            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            await Task.CompletedTask;
            var index = users.FindIndex(u => u.Id == id);

            if (index > -1)
            {
                users.RemoveAt(index);
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<UserDto>> ReadAllAsync()
        {
            await Task.CompletedTask;

            return users;
        }

        public async Task<UserDto?> ReadAsync(long id)
        {
            await Task.CompletedTask;
            var user = users.FirstOrDefault(u => u.Id == id);

            return user;
        }

        public async Task<bool> UpdateAsync(long id, UpdateUserDto updateUserDto)
        {
            await Task.CompletedTask;

            var index = users.FindIndex(u => u.Id == id);
            if (index > -1)
            {
                users[index] = new UserDto(id, updateUserDto.UserName, updateUserDto.FirstName, updateUserDto.LastName);
                return true;
            }
            return false;
        }
    }
}
