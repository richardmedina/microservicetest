using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.Contract.Model.User
{
    public record UserModel(long Id, string UserName, string FirstName, string LastName);
}
