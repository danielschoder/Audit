using Audit.Application.Interfaces.Persistence;
using Audit.Domain.Entities;
using Audit.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Audit.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
    : DbContext(options), IApplicationDbContext
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";

    public DbSet<DbContentChange> DbContentChanges { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DbContentChangeConfiguration.Configure(modelBuilder.Entity<DbContentChange>());

        base.OnModelCreating(modelBuilder);
    }
}
