using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parqueadero.Data.Migrations
{
    /// <inheritdoc />
    public partial class CambioPropiedadVehiculo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAPAgar",
                table: "Reservas");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAPAgar",
                table: "Motos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAPAgar",
                table: "Carros",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAPAgar",
                table: "Bicicletas",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAPAgar",
                table: "Motos");

            migrationBuilder.DropColumn(
                name: "TotalAPAgar",
                table: "Carros");

            migrationBuilder.DropColumn(
                name: "TotalAPAgar",
                table: "Bicicletas");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAPAgar",
                table: "Reservas",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
