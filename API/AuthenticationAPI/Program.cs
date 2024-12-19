

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

var ClientUrl = builder.Configuration.GetConnectionString("ClientUrl");
app.UseCors(x =>
{
    x.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins($"http://{ClientUrl}", $"https://{ClientUrl}");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
