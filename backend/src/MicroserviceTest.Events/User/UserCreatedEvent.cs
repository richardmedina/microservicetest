using MicroserviceTest.Contract.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.Events.User
{
    [MessageConsumerTopic("usercreated")]
    public record UserCreatedEvent(string Id, string UserName, string Password) : IEvent;
}
