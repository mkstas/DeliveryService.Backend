using Application;
using Infrastructure.AuthService;
using Infrastructure.CacheService;
using Infrastructure.EmailService;
using Infrastructure.Persistence;
using Presentation.Common;
using Presentation.Extensions;
using Presentation.Middlewares;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
    options.Conventions.Add(new RoutePrefixConvention("api/v1")));

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddCorsConfiguration(builder.Configuration);

builder.Services.AddOpenApi();

builder.Services.AddAuthService(builder.Configuration);
builder.Services.AddApplicationService();
builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddCacheService(builder.Configuration);
builder.Services.AddEmailService(builder.Configuration);

var app = builder.Build();

app.UseCors();

app.UseMiddleware<ExceptionHandler>();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    await PersistenceInitializer.InitAsync(app.Services);

    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();

app.Run();
