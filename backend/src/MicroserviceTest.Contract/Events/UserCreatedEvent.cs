using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.Contract.Events
{
    public record UserCreatedEvent(string Id, string UserName, string Password) : IEvent;
}
