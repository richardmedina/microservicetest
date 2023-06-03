using MicroserviceTest.Api.Email;
using MicroserviceTest.Api.Email.EventHandlers;
using MicroserviceTest.Common.Handlers;
using MicroserviceTest.Contract.Events;
using MicroserviceTest.Services;
using MicroserviceTest.CoreServices;
using MicroserviceTest.Api.Email.HostedServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IEventHandler<UserCreatedEvent>, UserCreatedEventHandler>();
builder.Services.AddMessageProducer();
builder.Services.AddMessageConsumer(options => { 
    options.Topics = new []{ "usercreated" };
});
builder.Services.AddHostedService<MessageConsumerBackgroundService>();

builder.Services.RegisterBusinessServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();