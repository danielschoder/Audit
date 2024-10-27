using Audit.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Audit.Infrastructure.Persistence.Configurations;

public class DbContentChangeConfiguration
{
    public static void Configure(EntityTypeBuilder<DbContentChange> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.EntityId).HasMaxLength(1023);
        builder.Property(e => e.EntityName).HasMaxLength(1023);
        builder.Property(e => e.FieldName).HasMaxLength(1023);
        builder.Property(e => e.OldContent).HasMaxLength(1023);
        builder.Property(e => e.NewContent).HasMaxLength(1023);
        builder.Property(e => e.ChangedBy).HasMaxLength(1023);
        builder.Property(e => e.ChangedById).HasMaxLength(1023);
    }
}
