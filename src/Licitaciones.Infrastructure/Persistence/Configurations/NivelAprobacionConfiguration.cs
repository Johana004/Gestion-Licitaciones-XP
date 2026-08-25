using Licitaciones.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Licitaciones.Infrastructure.Persistence.Configurations;

public class NivelAprobacionConfiguration : IEntityTypeConfiguration<NivelAprobacion>
{
    public void Configure(EntityTypeBuilder<NivelAprobacion> builder)
    {
        // ... configuraciones de la tabla ...

        var now = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        builder.HasData(
            new 
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                MontoMinimoCRC = 0.01m,
                MontoMaximoCRC = (decimal?)999999.99m,
                Aprobador = "Encargado de área",
                CreatedAt = now,
                UpdatedAt = now // <-- Agregar esta línea
            },
            new 
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                MontoMinimoCRC = 1000000.00m,
                MontoMaximoCRC = (decimal?)9999999.99m,
                Aprobador = "Gerencia",
                CreatedAt = now,
                UpdatedAt = now // <-- Agregar esta línea
            },
            new 
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                MontoMinimoCRC = 10000000.00m,
                MontoMaximoCRC = (decimal?)null,
                Aprobador = "Junta Directiva",
                CreatedAt = now,
                UpdatedAt = now // <-- Agregar esta línea
            }
        );
    }
}