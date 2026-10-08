using DeliveryService.Application;
using DeliveryService.Persistence.Postgres;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString(nameof(DeliveryServiceDbContext))
    ?? throw new InvalidOperationException($"Connection string '{nameof(DeliveryServiceDbContext)}' not found.");

builder.Services.AddPersistencePostgresLayer(connectionString);
builder.Services.AddApplicationLayer();

var app = builder.Build();

app.Run();
