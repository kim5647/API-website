using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data.SqlClient;

[DbContext(typeof(DBSave))]
public class DBSaveModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Login).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
        });
    }
}

public class DBSave : DbContext
{
    public DBSave(DbContextOptions<DBSave> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    // Не динамическая настройка модели, а использование скомпилированной модели
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-2FI2USO\\SQLEXPRESS;Database=API-website_BD;Trusted_Connection=True;");
        }
    }

    // Удаление метода динамической генерации модели
    //protected override void OnModelCreating(ModelBuilder modelBuilder) { }
}
