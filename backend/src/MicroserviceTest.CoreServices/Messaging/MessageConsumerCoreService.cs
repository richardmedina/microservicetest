using Confluent.Kafka;
using MicroserviceTest.Common.Attributes;
using MicroserviceTest.Common.Core.Messaging;
using MicroserviceTest.Common.Handlers;
using MicroserviceTest.Contract.Core.Messaging;
using MicroserviceTest.Contract.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MicroserviceTest.CoreServices.Messaging
{
    public class MessageConsumerCoreService : IMessageConsumerCoreService
    {
        private readonly ILogger<MessageConsumerCoreService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private Dictionary<string, Type> topicEventMappings = new Dictionary<string, Type>();

        private ConsumerConfig consumerConfig = new ConsumerConfig
        {
            BootstrapServers = "192.168.0.200:9092",
            GroupId = "grouId"
        };

        private ConsumerBuilder<string, string> consumerBuilder;
        private IConsumer<string, string> consumer;

        private readonly MessageConsumerOptions _messageConsumerOptions; 

        public MessageConsumerCoreService(IOptions<MessageConsumerOptions> options, ILogger<MessageConsumerCoreService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

            consumerBuilder = new ConsumerBuilder<string, string>(consumerConfig);
            _messageConsumerOptions = options.Value;
            consumer = consumer = consumerBuilder.Build();
            topicEventMappings = options.Value.EventTypes.ToDictionary(x => GetTopicNameFromEventType(x), y => y);

            consumer.Subscribe(topicEventMappings.Keys);
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
