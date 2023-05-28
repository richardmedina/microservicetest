using Confluent.Kafka;
using MicroserviceTest.Common.Core.Messaging;
using MicroserviceTest.Common.Handlers;
using MicroserviceTest.Contract.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MicroserviceTest.CoreServices.Messaging
{
    public class EventBus : IEventBus
    {
        private readonly ILogger<EventBus> _logger;
        private ConsumerConfig consumerConfig = new ConsumerConfig {
            BootstrapServers = "192.168.0.200:9092",
            GroupId = "grouId"
        };

        private ProducerConfig producerConfig = new ProducerConfig
        {
            BootstrapServers = "192.168.0.200:9092",
            ClientId = "groupId",
            //MessageTimeoutMs = 5000,
        };

        private string topic = "usercreated";

        public EventBus(ILogger<EventBus> logger)
        {
            _logger = logger;
        }

        public void Consume<TEvent>(CancellationToken cancelationToken) where TEvent : IEvent
        {
            _logger.LogInformation("Entry BackgroundService.Consume...");
            _logger.LogInformation("Creating consumerBuilder...");
            var consumerBuilder = new ConsumerBuilder<Ignore, string>(consumerConfig);
            _logger.LogInformation("Building consumer...");

            using (var consumer = consumerBuilder.Build())
            {
                consumer.Subscribe(topic);

                _logger.LogInformation("Starting Consume loop...");
                while (!cancelationToken.IsCancellationRequested)
                {
                    var value = consumer.Consume(cancelationToken);

                    _logger.LogInformation($"Consumed: {value.Message.Key}: {value.Message.Value}");
                }
            }
            _logger.LogInformation("Leaving BackgroundService.Consume...");
        }

        public void Publish<TEvent>(TEvent evt) where TEvent : IEvent
        {
            var producerBuilder = new ProducerBuilder<Null, string>(producerConfig);
            using (var producer = producerBuilder.Build())
            {
                var message = new Message<Null, string> {
                    Value = JsonSerializer.Serialize<TEvent>(evt)
                };
                var result = producer.ProduceAsync(topic, message);
                var cResult = result.Result;
            }
        }

    }
}
