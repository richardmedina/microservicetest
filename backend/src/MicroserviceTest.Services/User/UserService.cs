using MicroserviceTest.Common.Core.Data;
using MicroserviceTest.Common.Services;
using MicroserviceTest.Contract.Core.Data;
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
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CreateAsync(CreateUserDto createUserDto)
        {
            var userData = new UserData
            {
                UserName = createUserDto.UserName,
                Password = createUserDto.Password,
            };

            await _userRepository.CreateAsync(userData);
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UserDto>> ReadAllAsync()
        {
            var userDatas = await _userRepository.ReadAsync();

            return userDatas.Select(userData => new UserDto(userData.Id, userData.UserName, userData.Password));
        }

        public async Task<UserDto?> ReadAsync(string id)
        {
            var user = await _userRepository.ReadAsync(id);

            return user != null
                ? new UserDto(user.Id, user.UserName, user.Password)
                : null;
        }

        public async Task<bool> UpdateAsync(UpdateUserDto updateUserDto)
        {
            var userData = new UserData
            {
                Id = updateUserDto.Id,
                UserName = updateUserDto.UserName,
                Password = updateUserDto.Password
            };

            return await _userRepository.UpdateAsync(userData);
        }
    }
}
