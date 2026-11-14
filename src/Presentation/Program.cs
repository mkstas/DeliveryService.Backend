using Application;
using Infrastructure.CacheService;
using Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationService();
builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddCacheService(builder.Configuration);

var app = builder.Build();

app.Run();
