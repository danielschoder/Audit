using Audit.Application.Interfaces.Persistence;
using Audit.Domain.Entities;
using Audit.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Audit.Infrastructure.Persistence;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IConfiguration configuration)
    : DbContext(options), IApplicationDbContext
{
    private readonly IConfiguration _configuration = configuration;

    public DbSet<DbContentChange> DbContentChanges { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection") ?? "");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DbContentChangeConfiguration.Configure(modelBuilder.Entity<DbContentChange>());

        base.OnModelCreating(modelBuilder);
    }
}
