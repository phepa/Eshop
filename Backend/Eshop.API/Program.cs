using Eshop.API.Publishers;
using Eshop.Database;
using Eshop.Internal.Services;
using Eshop.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add database
builder.Services.AddDbContext<EshopDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LocalhostConnectionString")));

// Add Mass transit RabbitMQ
builder.Services.AddMassTransitMQ(typeof(Program).Assembly);

// Add services
builder.Services.AddScoped<ProductService>();
builder.Services.AddSingleton<NotificationPublisher>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

// Apply database migrations
app.MigrateDatabase<EshopDbContext>();

app.Run();
