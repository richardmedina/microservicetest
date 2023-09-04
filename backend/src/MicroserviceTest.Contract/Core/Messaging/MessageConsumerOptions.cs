using MicroserviceTest.Contract.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.Contract.Core.Messaging
{
    public class MessageConsumerOptions
    {
        public IEnumerable<Type> EventTypes = null!;

        public void SubscribeToEvent<TEvent>(TEvent tEvent) where TEvent : IEvent
        {

        }
    }
}
