using Licitaciones.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Licitaciones.Infrastructure.Persistence.Configurations;

public class TipoCambioConfiguration : IEntityTypeConfiguration<TipoCambio>
{
    public void Configure(EntityTypeBuilder<TipoCambio> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.CRCporUSD)
            .HasPrecision(18, 2)
            .IsRequired();

        var now = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        builder.HasData(
            new 
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                CRCporUSD = 505.50m,
                FechaVigencia = now,
                Activo = true,
                CreatedAt = now,
                UpdatedAt = (DateTimeOffset?)null
            }
        );
    }
}