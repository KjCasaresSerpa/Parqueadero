using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parqueadero.Data.Migrations
{
    /// <inheritdoc />
    public partial class NuevaReserva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TotalAPAgar",
                table: "Reservas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAPAgar",
                table: "Reservas");
        }
    }
}
