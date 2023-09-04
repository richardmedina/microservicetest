using MicroserviceTest.Contract.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.Common.Handlers
{
    public interface IEventHandler
    {
    }

    public interface IEventHandler<TEvent>: IEventHandler where TEvent : IEvent
    {
        Task<bool> HandleAsync(TEvent evt);
    }
}
