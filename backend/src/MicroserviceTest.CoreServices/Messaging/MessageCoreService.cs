using MicroserviceTest.Common.Core.Messaging;
using MicroserviceTest.Common.Handlers;
using MicroserviceTest.Contract.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.CoreServices.Messaging
{
    public class MessageCoreService : IMessageCoreService
    {
        public void Publish<TEvent>(TEvent evt) where TEvent : IEvent
        {
            throw new NotImplementedException();
        }

        public void Subscribe<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent
        {
            throw new NotImplementedException();
        }
    }
}
