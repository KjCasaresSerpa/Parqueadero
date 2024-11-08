using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parqueadero.Data.Migrations
{
    /// <inheritdoc />
    public partial class correccionesEnTarifa : Migration
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

            migrationBuilder.AlterColumn<int>(
                name: "TarifaId",
                table: "Motos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TarifaId",
                table: "Carros",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TarifaId",
                table: "Bicicletas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "TarifaId",
                table: "Motos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TarifaId",
                table: "Carros",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TarifaId",
                table: "Bicicletas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bicicletas_Tarifas_TarifaId",
                table: "Bicicletas",
                column: "TarifaId",
                principalTable: "Tarifas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carros_Tarifas_TarifaId",
                table: "Carros",
                column: "TarifaId",
                principalTable: "Tarifas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Motos_Tarifas_TarifaId",
                table: "Motos",
                column: "TarifaId",
                principalTable: "Tarifas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
