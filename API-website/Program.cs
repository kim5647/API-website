using Microsoft.EntityFrameworkCore;
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
//builder.Services.AddDbContext<DBSave>(options =>
//    options.UseSqlServer("Data Source=DESKTOP-2FI2USO//SQLEXPRESS;Initial Catalog=API-website_BD;Integrated Security=True;Encrypt=False;"));


// ��������� Kestrel ��� ������������� ������������� IP-������ � �����
builder.WebHost.UseKestrel(options =>
{
    options.Listen(IPAddress.Parse("26.234.86.94"), 8080); // ���������� IP � ����
});

var app = builder.Build();

app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();
