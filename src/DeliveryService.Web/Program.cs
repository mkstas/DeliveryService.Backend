using DeliveryService.Persistence.Postgres;
using DeliveryService.Persistence.Postgres.Common;
using DeliveryService.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.AddCorsConfiguration();
builder.AddJwtConfiguration();

builder.AddWebAuthentification();

builder.AddDatabasePostgres();
builder.AddInfrastructure();
builder.AddApplication();

var app = builder.Build();

app.UseCors();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<DeliveryServiceDbContext>();
context.Database.Migrate();
await new PostgresDatabaseSeeder(context).SeedAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
