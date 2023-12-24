using Confluent.Kafka;
using MicroserviceTest.Common.Core.Messaging;
using Microsoft.Extensions.Configuration;
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
        private readonly ILogger<MessageProducerCoreService> _logger;
        private readonly IConfiguration _configuration;

        private IProducer<string, string>? producer;

        private string topic = "usercreated";

        public MessageProducerCoreService(ILogger<MessageProducerCoreService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task PublishAsync(string topic, string key, string value, CancellationToken cancelationToken = default)
        {
            _logger.LogInformation("PublishAsync Start: " + JsonSerializer.Serialize(new { topic, key, value }));

            if(producer == null)
            {
                var producerBuilder = new ProducerBuilder<string, string>(CreateKafkaProducerConfig());
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

        private ProducerConfig CreateKafkaProducerConfig()
        {
            var section = _configuration.GetSection("Kafka");
            var bootstrapServers = section["BootstrapServers"] ?? string.Empty;
            var clientId = section["ClientId"] ?? string.Empty;

            return new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
                ClientId = clientId,
            };
        }
    }
}
