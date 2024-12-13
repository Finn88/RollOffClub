
using GatewayAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.AddIdentityServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(x =>
{
    x.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins("http://localhost:4200", "https://localhost:4200");
});
app.MapReverseProxy();
app.UseAuthentication();
app.UseAuthorization();
app.Run();
