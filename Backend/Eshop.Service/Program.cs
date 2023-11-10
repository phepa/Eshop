using Eshop.Service;
using Eshop.Service.Background_services;
using Eshop.Shared.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<NotificationService>();

builder.Services.AddSingleton<PersistanceHelper>();

// Add Mass transit RabbitMQ
builder.Services.AddMassTransitMQ(typeof(Program).Assembly);

WebApplication app = builder.Build();

app.Run();
