using MicroserviceTest.Contract.Core.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.Common.Core.Messaging
{
    public interface IMessageConsumerCoreService
    {
        Task<ConsumerMessage> ConsumeAsync(CancellationToken cancellationToken);
    }
}
