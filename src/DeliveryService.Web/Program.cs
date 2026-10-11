using DeliveryService.Application;
using DeliveryService.Infrastructure;
using DeliveryService.Persistence.Postgres;
using DeliveryService.Persistence.Postgres.Common;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString(nameof(DeliveryServiceDbContext))
    ?? throw new InvalidOperationException($"Connection string '{nameof(DeliveryServiceDbContext)}' not found.");

builder.Services.AddPersistencePostgresLayer(connectionString);
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<DeliveryServiceDbContext>();
context.Database.Migrate();
await new DbSeeder(context).SeedAsync();

app.Run();
