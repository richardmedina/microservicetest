using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.Contract.Dtos.User
{
    public record UserDto(long Id, string UserName, string FirstName, string LastName);
}
