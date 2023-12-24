using Amazon.Util.Internal.PlatformServices;
using Confluent.Kafka;
using MicroserviceTest.Common.Attributes;
using MicroserviceTest.Common.Core.Messaging;
using MicroserviceTest.Common.Handlers;
using MicroserviceTest.Contract.Core.Messaging;
using MicroserviceTest.Contract.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MicroserviceTest.CoreServices.Messaging
{
    public class MessageConsumerCoreService : IMessageConsumerCoreService
    {
        private readonly ILogger<MessageConsumerCoreService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private Dictionary<string, Type> topicEventMappings = new Dictionary<string, Type>();

        //private ConsumerBuilder<string, string> consumerBuilder;
        private IConsumer<string, string> consumer;

        private readonly MessageConsumerOptions _messageConsumerOptions; 

        public MessageConsumerCoreService(
            IOptions<MessageConsumerOptions> options, 
            ILogger<MessageConsumerCoreService> logger, 
            IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            _messageConsumerOptions = options?.Value ?? new MessageConsumerOptions();
            _logger = logger;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            consumer = CreateKafkaConsumer();
        }

        private IConsumer<string, string> CreateKafkaConsumer()
        {
            var consumerBuilder = new ConsumerBuilder<string, string>(CreateKafkaConsumerConfig());
            
            consumer = consumerBuilder.Build();
            topicEventMappings = _messageConsumerOptions.EventTypes.ToDictionary(x => GetTopicNameFromEventType(x), y => y);

            consumer.Subscribe(topicEventMappings.Keys);

            return consumer;
        }

        private ConsumerConfig CreateKafkaConsumerConfig()
        {
            var section = _configuration.GetSection("Kafka");
            var bootstrapServers = section["BootstrapServers"] ?? string.Empty;
            var groupId = section["GroupId"] ?? string.Empty;

            return new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = groupId,
            };
        }

        public async Task StartConsumingAsync(CancellationToken cancellationToken)
        {
            await Task.Yield();
            _logger.LogInformation("ConsumeAsync Start: ");

            while (!cancellationToken.IsCancellationRequested)
            {
                var consumeResult = consumer.Consume(cancellationToken);
                var message = new ConsumerMessage(consumeResult.Topic, consumeResult.Message.Key, consumeResult.Message.Value);

                if (await processConsumerMessage(message))
                {
                    consumer.Commit(consumeResult);
                }
                _logger.LogInformation("Message Received: " + JsonSerializer.Serialize(new { consumeResult }));
            }
            _logger.LogInformation("Closing Consumer...");
        }

        private async Task<bool> processConsumerMessage(ConsumerMessage message)
        {
            bool invokeResult = false;
            var eventType = topicEventMappings[message.Topic];
            object? payload = JsonSerializer.Deserialize(message.Value, topicEventMappings[message.Topic]);

            if (payload != null)
            {
                invokeResult = await executeEventHandlerAsync(eventType, payload);
            }

            return invokeResult;
        }

        private async Task<bool> executeEventHandlerAsync(Type eventType, object payload)
        {
            await Task.CompletedTask;

            var eventHandlerGenericType = typeof(IEventHandler<>).MakeGenericType(eventType);
            var eventHandler = _serviceProvider.GetService(eventHandlerGenericType);
            var eventHandlerMethod = eventHandlerGenericType.GetMethod("HandleAsync");
            var eventHandlerMethodParameters = new[] { payload };

            var task = eventHandlerMethod?.Invoke(eventHandler, eventHandlerMethodParameters) as Task<bool>;
            var invokeResult = task == null ? false : task.Result == true;
            
            return invokeResult;
        }


        private string GetTopicNameFromEventType(Type type)
        {
            return type.GetCustomAttribute<MessageConsumerTopicAttribute>()?.Topic
                ?? type.FullName ?? string.Empty;
        }
    }
}
