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

        // Los montos monetarios o tasas de conversión siempre deben configurarse con precisión decimal explícita (18,2 o 18,4)
        builder.Property(tc => tc.Valor)
            .IsRequired()
            .HasColumnType("decimal(18,4)");

        builder.Property(tc => tc.FechaVigencia)
            .IsRequired();

        builder.Property(tc => tc.Activo)
            .IsRequired();
    }
}