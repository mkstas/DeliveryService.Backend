using Application;
using DotNetEnv;
using Infrastructure.AuthService;
using Infrastructure.CacheService;
using Infrastructure.EmailService;
using Infrastructure.Persistence;
using Infrastructure.StorageService;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common;
using Presentation.Extensions;
using Presentation.Middlewares;
using Scalar.AspNetCore;

Env.TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
    options.Conventions.Add(new RoutePrefixConvention("api/v1")))
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
            new BadRequestObjectResult(ErrorResponseFactory.FromModelState(context.ModelState));
    });

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddCorsConfiguration(builder.Configuration);

builder.Services.AddOpenApi();

builder.Services.AddAuthService(builder.Configuration);
builder.Services.AddApplicationService();
builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddCacheService(builder.Configuration);
builder.Services.AddEmailService(builder.Configuration);
builder.Services.AddStorageService(builder.Configuration);

var app = builder.Build();

await PersistenceInitializer.InitAsync(app.Services);

app.UseCors();

app.UseMiddleware<ExceptionHandler>();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();

app.Run();
