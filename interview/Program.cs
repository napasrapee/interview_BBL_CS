var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseHttpsRedirection();

// แสดงคำว่า Hello World ที่หน้าแรกของ localhost
app.MapGet("/", () => "Hello World");

app.Run();
