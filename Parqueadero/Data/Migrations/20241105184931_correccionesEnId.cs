using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parqueadero.Data.Migrations
{
    /// <inheritdoc />
    public partial class correccionesEnId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Motos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Carros",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Bicicletas",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Motos",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Carros",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Bicicletas",
                newName: "id");
        }
    }
}
