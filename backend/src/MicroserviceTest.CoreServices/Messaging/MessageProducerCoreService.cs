using Confluent.Kafka;
using MicroserviceTest.Common.Core.Messaging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MicroserviceTest.CoreServices.Messaging
{
    public class MessageProducerCoreService : IMessageProducerCoreService
    {
        private ILogger<MessageProducerCoreService> _logger;
        private ProducerConfig producerConfig = new ProducerConfig
        {
            BootstrapServers = "192.168.0.200:9092",
            ClientId = "groupId",
            //MessageTimeoutMs = 5000,
        };

        private readonly ProducerBuilder<string, string> producerBuilder;
        private IProducer<string, string>? producer;

        private string topic = "usercreated";

        public MessageProducerCoreService(ILogger<MessageProducerCoreService> logger)
        {
            _logger = logger;
            producerBuilder = new ProducerBuilder<string, string>(producerConfig);
        }

        public async Task PublishAsync(string topic, string key, string value, CancellationToken cancelationToken = default)
        {
            _logger.LogInformation("PublishAsync Start: " + JsonSerializer.Serialize(new { topic, key, value }));

            if(producer == null)
            {
                producer = producerBuilder.Build();
            }

            var message = new Message<string, string>
            {
                Key = key,
                Value = value
            };

            var result = await producer.ProduceAsync(topic, message, cancelationToken);

            _logger.LogInformation("PublishAsync End: Success");
        }

        public async Task PublishAsync<TMessage>(string topic, TMessage message, CancellationToken cancelationToken = default)
        {
            _logger.LogInformation("PublishAsync<TMessage> Start: " + JsonSerializer.Serialize(new { topic, message }));

            var key = typeof(TMessage).FullName;
            var value = JsonSerializer.Serialize(message);

            if(key == null) { return; }

            await PublishAsync(topic, key, value, cancelationToken);
            _logger.LogInformation("PublishAsync<TMessage> End: Success");
        }
    }
}
