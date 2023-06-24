using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.Contract.Model.User
{
    public record CreateUserModel(string Id, string UserName, string Password);
}
