using Licitaciones.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Licitaciones.Infrastructure.Persistence.Configurations;

public class NivelAprobacionConfiguration : IEntityTypeConfiguration<NivelAprobacion>
{
    public void Configure(EntityTypeBuilder<NivelAprobacion> builder)
    {
        builder.ToTable("NivelesAprobacion");

        builder.HasKey(na => na.Id);

        builder.Property(na => na.MontoMinimoCRC)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(na => na.MontoMaximoCRC)
            .HasColumnType("decimal(18,2)"); // Nullable para la Junta Directiva

        builder.Property(na => na.Aprobador)
            .IsRequired()
            .HasMaxLength(100);

        // Seed data oficial exigido en la rúbrica del proyecto
       builder.HasData(
    new
    { 
        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), 
        MontoMinimoCRC = 0.01m, 
        MontoMaximoCRC = (decimal?)999999.99m, 
        Aprobador = "Encargado de área"
    },
    new
    { 
        Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), 
        MontoMinimoCRC = 1000000.00m, 
        MontoMaximoCRC = (decimal?)9999999.99m, 
        Aprobador = "Gerencia"
    },
    new
    { 
        Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), 
        MontoMinimoCRC = 10000000.00m, 
        MontoMaximoCRC = (decimal?)null, 
        Aprobador = "Junta Directiva"
    }
);
    }
}   