using MicroserviceTest.Services;
using MicroserviceTest.CoreServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterBusinessServices();
builder.Services.AddMongoDb(builder.Configuration.GetSection("Mongo"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMessageProducer();

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
