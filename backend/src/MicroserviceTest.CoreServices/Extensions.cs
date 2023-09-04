using MicroserviceTest.Common.Core.Data;
using MicroserviceTest.Common.Core.Messaging;
using MicroserviceTest.Contract.Core.Messaging;
using MicroserviceTest.CoreServices.Data;
using MicroserviceTest.CoreServices.Data.Options;
using MicroserviceTest.CoreServices.Messaging;
using MicroserviceTest.CoreServices.Messaging.HostedServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicroserviceTest.CoreServices
{
    public static class Extensions
    {
        public static void AddMessageProducer(this IServiceCollection services)
        {
            services.AddScoped<IMessageProducerCoreService, MessageProducerCoreService>();
        }

        public static void AddMessageConsumer(this IServiceCollection services, Action<MessageConsumerOptions> options)
        {
            services.Configure(options);
            services.AddSingleton<IMessageConsumerCoreService, MessageConsumerCoreService>();
            services.AddHostedService<MessageConsumerBackgroundService>();
        }

        public static void AddMongoDb(this IServiceCollection services, IConfigurationSection configurationSection)
        {
            var mongoDbOptions = GetMongoDbOptions(configurationSection);

            services.Configure<MongoDbOptions>(options => {
                options.ConnectionString = mongoDbOptions.ConnectionString;
                options.Database = mongoDbOptions.Database;
                });

            AddMongoDb(services);
        }

        private static void AddMongoDb(IServiceCollection services)
        {
            services.AddScoped<IMongoDatabase, MongoDatabase>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        private static MongoDbOptions GetMongoDbOptions(IConfigurationSection configurationSection)
        {
            var connectionString = configurationSection != null
                ? configurationSection["connectionString"] ?? string.Empty
                : string.Empty;

            var database = configurationSection != null
                ? configurationSection["database"] ?? string.Empty
                : string.Empty;

            var mongoDbOptions = new MongoDbOptions
            {
                ConnectionString = connectionString,
                Database = database,
            };

            return mongoDbOptions;
        }
    }
}
