using MicroserviceTest.Common.Handlers;
using MicroserviceTest.Contract.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.Common.Core.Messaging
{
    public interface IMessageCoreService
    {
        
        void Publish<TEvent>(TEvent evt) where TEvent : IEvent;
    }
}
