using API.Extensions;
using Application.Core;
using Application.Organizations;
using Infrastructure.Data; 
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(opt => {
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(List.Handler).Assembly));
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.AddAuthAuthenticationGoogle();
builder.AddAuthAuthenticationFacebook();


var app = builder.Build();

app.UseCors(x =>
{
    x.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins("http://localhost:4200", "https://localhost:4200");
});


app.UseAuthentication();
app.UseAuthorization();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<DataContext>();
context.Database.Migrate();



app.UseHttpsRedirection();
app.MapControllers();
app.Run();
