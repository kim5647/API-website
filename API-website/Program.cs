using System.Net; // ��������� ��� ������������� IPAddress
using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

// ��������� CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// ���������� ������������
builder.Services.AddControllers();

// ��������� Kestrel ��� ������������� ������������� IP-������ � �����
builder.WebHost.UseKestrel(options =>
{
    options.Listen(IPAddress.Parse("26.197.1.24"), 8080); // ���������� IP � ����
});

var app = builder.Build();

app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();
