using System.Net; // Добавлено для использования IPAddress
using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

// Настройка CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// Добавление контроллеров
builder.Services.AddControllers();

// Настройка Kestrel для использования определенного IP-адреса и порта
builder.WebHost.UseKestrel(options =>
{
    options.Listen(IPAddress.Parse("26.197.1.24"), 8080); // Установите IP и порт
});

var app = builder.Build();

app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();
