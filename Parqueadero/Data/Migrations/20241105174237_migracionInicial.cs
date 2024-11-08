using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parqueadero.Data.Migrations
{
    /// <inheritdoc />
    public partial class migracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EspaciosParkings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantidadEspacios = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspaciosParkings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Placa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoraEstimadaLlegada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoVehiculo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tarifas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CostoPorHora = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TipoVehiculo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarifas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bicicletas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Placa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoraEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraSalida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TarifaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bicicletas", x => x.id);
                    table.ForeignKey(
                        name: "FK_Bicicletas_Tarifas_TarifaId",
                        column: x => x.TarifaId,
                        principalTable: "Tarifas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carros",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Placa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoraEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraSalida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TarifaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carros", x => x.id);
                    table.ForeignKey(
                        name: "FK_Carros_Tarifas_TarifaId",
                        column: x => x.TarifaId,
                        principalTable: "Tarifas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Motos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Placa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoraEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraSalida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TarifaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Motos_Tarifas_TarifaId",
                        column: x => x.TarifaId,
                        principalTable: "Tarifas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bicicletas_TarifaId",
                table: "Bicicletas",
                column: "TarifaId");

            migrationBuilder.CreateIndex(
                name: "IX_Carros_TarifaId",
                table: "Carros",
                column: "TarifaId");

            migrationBuilder.CreateIndex(
                name: "IX_Motos_TarifaId",
                table: "Motos",
                column: "TarifaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bicicletas");

            migrationBuilder.DropTable(
                name: "Carros");

            migrationBuilder.DropTable(
                name: "EspaciosParkings");

            migrationBuilder.DropTable(
                name: "Motos");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Tarifas");
        }
    }
}
