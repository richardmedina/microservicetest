using Confluent.Kafka;
using MicroserviceTest.Common.Core.Messaging;
using MicroserviceTest.Contract.Core.Messaging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MicroserviceTest.CoreServices.Messaging
{
    public class MessageConsumerCoreService : IMessageConsumerCoreService
    {
        private readonly ILogger<MessageConsumerCoreService> _logger;

        private ConsumerConfig consumerConfig = new ConsumerConfig
        {
            BootstrapServers = "192.168.0.200:9092",
            GroupId = "grouId"
        };

        private ConsumerBuilder<string, string> consumerBuilder;
        private IConsumer<string, string> consumer;

        private readonly MessageConsumerOptions _messageConsumerOptions; 

        public MessageConsumerCoreService(IOptions<MessageConsumerOptions> options, ILogger<MessageConsumerCoreService> logger)
        {
            _logger = logger;
            consumerBuilder = new ConsumerBuilder<string, string>(consumerConfig);
            _messageConsumerOptions = options.Value;
            consumer = consumer = consumerBuilder.Build();
            consumer.Subscribe(_messageConsumerOptions.Topics);
        }

        public async Task<ConsumerMessage> ConsumeAsync(CancellationToken cancellationToken)
        {
            await Task.Yield();
            _logger.LogInformation("ConsumeAsync Start: ");

            var consumeResult = consumer.Consume(cancellationToken);
            var message = new ConsumerMessage(consumeResult.Topic, consumeResult.Message.Key, consumeResult.Message.Value);
            consumer.Commit(consumeResult);

            _logger.LogInformation("Message Received: " + JsonSerializer.Serialize(new { consumeResult }));

            return message;
        }
    }
}
