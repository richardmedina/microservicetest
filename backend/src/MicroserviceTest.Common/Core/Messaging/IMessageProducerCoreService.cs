using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.Common.Core.Messaging
{
    public interface IMessageProducerCoreService
    {
        Task PublishAsync(string topic, string key, string value, CancellationToken cancelationToken = default);
        Task PublishAsync<TMessage>(string topic, TMessage message, CancellationToken cancelationToken = default);
    }
}
