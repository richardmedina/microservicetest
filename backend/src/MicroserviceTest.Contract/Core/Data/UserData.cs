using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.Contract.Core.Data
{
    public class UserData
    {
        public string Id { get; set; } = null!;
        public string UserName { get;set; } = null!;
        public string Password { get; set; } = null!;
    }
}
