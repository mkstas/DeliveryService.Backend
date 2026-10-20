using DeliveryService.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddDocumentaion();
builder.AddRouteConvention();

builder.AddCorsConfiguration();
builder.AddJwtConfiguration();

builder.AddAuthentification();
builder.AddRustFSClient();

builder.AddDatabasePostgres();
builder.AddApplication();
builder.AddInfrastructure();

var app = builder.Build();

app.UseExceptionHandler();

await app.SeedPostgresDatabase();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapDocumentation();

app.Run();
