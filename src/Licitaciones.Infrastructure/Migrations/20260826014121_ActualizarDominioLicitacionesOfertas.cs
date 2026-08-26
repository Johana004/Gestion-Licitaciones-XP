using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licitaciones.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarDominioLicitacionesOfertas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VersionConcurrencia",
                table: "Ofertas");

            migrationBuilder.RenameColumn(
                name: "CRCPorUSD",
                table: "TiposCambio",
                newName: "CRCporUSD");

            migrationBuilder.RenameColumn(
                name: "MontoOfertaCRC",
                table: "Ofertas",
                newName: "MontoOfertadoCRC");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "TiposCambio",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Ofertas",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Ofertas",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "NivelesAprobacion",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.UpdateData(
                table: "TiposCambio",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "UpdatedAt",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Ofertas_ProveedorId",
                table: "Ofertas",
                column: "ProveedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ofertas_Licitaciones_LicitacionId",
                table: "Ofertas",
                column: "LicitacionId",
                principalTable: "Licitaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ofertas_Proveedores_ProveedorId",
                table: "Ofertas",
                column: "ProveedorId",
                principalTable: "Proveedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ofertas_Licitaciones_LicitacionId",
                table: "Ofertas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ofertas_Proveedores_ProveedorId",
                table: "Ofertas");

            migrationBuilder.DropIndex(
                name: "IX_Ofertas_ProveedorId",
                table: "Ofertas");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Ofertas");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Ofertas");

            migrationBuilder.RenameColumn(
                name: "CRCporUSD",
                table: "TiposCambio",
                newName: "CRCPorUSD");

            migrationBuilder.RenameColumn(
                name: "MontoOfertadoCRC",
                table: "Ofertas",
                newName: "MontoOfertaCRC");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "TiposCambio",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "VersionConcurrencia",
                table: "Ofertas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "NivelesAprobacion",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "TiposCambio",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "UpdatedAt",
                value: new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
