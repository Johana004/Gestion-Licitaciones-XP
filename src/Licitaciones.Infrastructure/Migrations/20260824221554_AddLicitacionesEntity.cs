using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licitaciones.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLicitacionesEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Licitaciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CodigoNormalizado = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Titulo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Estado = table.Column<int>(type: "integer", nullable: false),
                    FechaCierre = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    PresupuestoEstimadoCRC = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    VersionConcurrencia = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licitaciones", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "TiposCambio",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "FechaVigencia",
                value: new DateTime(2026, 8, 24, 22, 15, 52, 874, DateTimeKind.Utc).AddTicks(423));

            migrationBuilder.CreateIndex(
                name: "IX_Licitaciones_CodigoNormalizado",
                table: "Licitaciones",
                column: "CodigoNormalizado",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Licitaciones");

            migrationBuilder.UpdateData(
                table: "TiposCambio",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "FechaVigencia",
                value: new DateTime(2026, 8, 11, 21, 2, 16, 648, DateTimeKind.Utc).AddTicks(2696));
        }
    }
}
