using MicroserviceTest.Contract.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.Common.Services
{
    public interface IUserService
    {
        Task<bool>CreateAsync(CreateUserDto createUserDto);
        Task<UserDto?> ReadAsync(string id);
        Task<IEnumerable<UserDto>> ReadAllAsync();
        Task<bool> UpdateAsync(UpdateUserDto updateUserDto);
        Task<bool> DeleteAsync(string id);
    }
}
