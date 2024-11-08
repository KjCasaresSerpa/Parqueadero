using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parqueadero.Data.Migrations
{
    /// <inheritdoc />
    public partial class correccionesEnTarifa2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bicicletas_Tarifas_TarifaId",
                table: "Bicicletas");

            migrationBuilder.DropForeignKey(
                name: "FK_Carros_Tarifas_TarifaId",
                table: "Carros");

            migrationBuilder.DropForeignKey(
                name: "FK_Motos_Tarifas_TarifaId",
                table: "Motos");

            migrationBuilder.DropIndex(
                name: "IX_Motos_TarifaId",
                table: "Motos");

            migrationBuilder.DropIndex(
                name: "IX_Carros_TarifaId",
                table: "Carros");

            migrationBuilder.DropIndex(
                name: "IX_Bicicletas_TarifaId",
                table: "Bicicletas");

            migrationBuilder.DropColumn(
                name: "TarifaId",
                table: "Motos");

            migrationBuilder.DropColumn(
                name: "TarifaId",
                table: "Carros");

            migrationBuilder.DropColumn(
                name: "TarifaId",
                table: "Bicicletas");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HoraEntrada",
                table: "Motos",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HoraEntrada",
                table: "Carros",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HoraEntrada",
                table: "Bicicletas",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "HoraEntrada",
                table: "Motos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TarifaId",
                table: "Motos",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "HoraEntrada",
                table: "Carros",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TarifaId",
                table: "Carros",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "HoraEntrada",
                table: "Bicicletas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TarifaId",
                table: "Bicicletas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Motos_TarifaId",
                table: "Motos",
                column: "TarifaId");

            migrationBuilder.CreateIndex(
                name: "IX_Carros_TarifaId",
                table: "Carros",
                column: "TarifaId");

            migrationBuilder.CreateIndex(
                name: "IX_Bicicletas_TarifaId",
                table: "Bicicletas",
                column: "TarifaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bicicletas_Tarifas_TarifaId",
                table: "Bicicletas",
                column: "TarifaId",
                principalTable: "Tarifas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carros_Tarifas_TarifaId",
                table: "Carros",
                column: "TarifaId",
                principalTable: "Tarifas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Motos_Tarifas_TarifaId",
                table: "Motos",
                column: "TarifaId",
                principalTable: "Tarifas",
                principalColumn: "Id");
        }
    }
}
