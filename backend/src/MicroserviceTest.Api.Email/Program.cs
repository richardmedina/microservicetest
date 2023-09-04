using MicroserviceTest.Api.Email.EventHandlers;
using MicroserviceTest.Common.Handlers;
using MicroserviceTest.Services;
using MicroserviceTest.CoreServices;
using MicroserviceTest.Common.Events.User;
using MicroserviceTest.Common.Events.Unknown;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IEventHandler<UserCreatedEvent>, UserCreatedEventHandler>();
builder.Services.AddSingleton<IEventHandler<UnknownEvent>, UnknownEventHandler>();
//builder.Services.AddMessageProducer();
builder.Services.AddMessageConsumer(options => {
    options.Topics = new[] { 
        typeof(UserCreatedEvent)
        //typeof(UnknownEvent) 
    };
});

builder.Services.RegisterBusinessServices();
builder.Services.AddMongoDb(builder.Configuration.GetSection("Mongo"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var swaggerEnabled = builder.Configuration.GetValue<bool>("SwaggerEnabled");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || swaggerEnabled)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();