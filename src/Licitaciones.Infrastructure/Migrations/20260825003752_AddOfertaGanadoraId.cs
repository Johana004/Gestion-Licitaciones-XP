using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licitaciones.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOfertaGanadoraId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OfertaGanadoraId",
                table: "Licitaciones",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "TiposCambio",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "FechaVigencia",
                value: new DateTime(2026, 8, 25, 0, 37, 50, 304, DateTimeKind.Utc).AddTicks(2014));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfertaGanadoraId",
                table: "Licitaciones");

            migrationBuilder.UpdateData(
                table: "TiposCambio",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "FechaVigencia",
                value: new DateTime(2026, 8, 24, 23, 42, 10, 104, DateTimeKind.Utc).AddTicks(334));
        }
    }
}
