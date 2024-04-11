using AuthApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Infra.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(opts =>
        {
            opts.HasKey(user => user.Id);
            opts.Property(user => user.Id)
                .HasColumnName("id");

            opts.Property(user => user.Username)
                .HasColumnType("varchar(100)")
                .HasColumnName("username");
            opts.Property(user => user.Password)
                .HasColumnType("varchar(200)")
                .HasColumnName("password");
            opts.Property(user => user.Name)
                .HasColumnType("varchar(100)")
                .HasColumnName("name");
            opts.Property(user => user.Avatar)
                .HasColumnName("avatar");
            opts.Property(user => user.Bio)
                .HasColumnName("bio");
            opts.Property(user => user.IsDelete)
                .HasColumnName("is_delete");
        });
    }
}
