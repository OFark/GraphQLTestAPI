using Microsoft.EntityFrameworkCore;
using TestWebAPI.Models;

namespace TestWebAPI;

public sealed class dBContext : DbContext
{
    private readonly IWebHostEnvironment environment;

    public dBContext(DbContextOptions<dBContext> options, IWebHostEnvironment environment) : base(options) => this.environment = environment;

    internal DbSet<DB_Login> Logins => Set<DB_Login>();

    internal DbSet<DB_Brand> Brands => Set<DB_Brand>();

    internal DbSet<DB_BrandContent> BrandContents => Set<DB_BrandContent>();

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<DB_Login>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

        modelBuilder.Entity<DB_Brand>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

        modelBuilder.Entity<DB_BrandContent>().Property(x => x.Id).HasDefaultValueSql("NEWID()");


        modelBuilder.Entity<DB_Login>()
            .HasOne(l => l.Brand)
            .WithMany(b => b.Logins)
            .HasForeignKey(l => l.BrandId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}