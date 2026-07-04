using interview.Interfaces;
using interview.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapGet("/", () => "Hello World");
app.MapControllers();

app.Run();
