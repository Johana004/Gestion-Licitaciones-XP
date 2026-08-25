using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licitaciones.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOfertasEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ofertas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LicitacionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProveedorId = table.Column<Guid>(type: "uuid", nullable: false),
                    MontoOfertaCRC = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    FechaPresentacion = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    VersionConcurrencia = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ofertas", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "TiposCambio",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "FechaVigencia",
                value: new DateTime(2026, 8, 24, 23, 42, 10, 104, DateTimeKind.Utc).AddTicks(334));

            migrationBuilder.CreateIndex(
                name: "IX_Ofertas_LicitacionId_ProveedorId",
                table: "Ofertas",
                columns: new[] { "LicitacionId", "ProveedorId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ofertas");

            migrationBuilder.UpdateData(
                table: "TiposCambio",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "FechaVigencia",
                value: new DateTime(2026, 8, 24, 22, 15, 52, 874, DateTimeKind.Utc).AddTicks(423));
        }
    }
}
