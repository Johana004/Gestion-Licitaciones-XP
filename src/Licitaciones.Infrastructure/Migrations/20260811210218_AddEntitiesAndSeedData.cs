using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Licitaciones.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEntitiesAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NivelesAprobacion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MontoMinimoCRC = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    MontoMaximoCRC = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    Aprobador = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NivelesAprobacion", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "NivelesAprobacion",
                columns: new[] { "Id", "Aprobador", "MontoMaximoCRC", "MontoMinimoCRC" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Encargado de área", 999999.99m, 0.01m },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Gerencia", 9999999.99m, 1000000.00m },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Junta Directiva", null, 10000000.00m }
                });

            migrationBuilder.InsertData(
                table: "TiposCambio",
                columns: new[] { "Id", "Activo", "FechaVigencia", "Valor" },
                values: new object[] { new Guid("44444444-4444-4444-4444-444444444444"), true, new DateTime(2026, 8, 11, 21, 2, 16, 648, DateTimeKind.Utc).AddTicks(2696), 520.0000m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NivelesAprobacion");

            migrationBuilder.DeleteData(
                table: "TiposCambio",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));
        }
    }
}
