using Licitaciones.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Licitaciones.Infrastructure.Persistence.Configurations;

public class TipoCambioConfiguration : IEntityTypeConfiguration<TipoCambio>
{
    public void Configure(EntityTypeBuilder<TipoCambio> builder)
    {
        builder.ToTable("TiposCambio");

        builder.HasKey(tc => tc.Id);

        builder.Property(tc => tc.Valor)
            .IsRequired()
            .HasColumnType("decimal(18,4)");

        builder.Property(tc => tc.FechaVigencia)
            .IsRequired();

        builder.Property(tc => tc.Activo)
            .IsRequired();

        // Seed data usando objeto anónimo para saltar setters privados
        builder.HasData(new
        {
            Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
            Valor = 520.0000m,
            FechaVigencia = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
            Activo = true
        });
    }
}