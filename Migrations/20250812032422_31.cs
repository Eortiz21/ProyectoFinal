using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoyectoParqueo.Migrations
{
    /// <inheritdoc />
    public partial class _31 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id_Cliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id_Cliente);
                });

            migrationBuilder.CreateTable(
                name: "EspaciosEstacionamiento",
                columns: table => new
                {
                    Id_Espacio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    No_Espacio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nivel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoEspacio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspaciosEstacionamiento", x => x.Id_Espacio);
                });

            migrationBuilder.CreateTable(
                name: "Tarifas",
                columns: table => new
                {
                    Id_Tarifa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoTarifa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarifas", x => x.Id_Tarifa);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    NoPlaca = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoVehiculo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Cliente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.NoPlaca);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Clientes_Id_Cliente",
                        column: x => x.Id_Cliente,
                        principalTable: "Clientes",
                        principalColumn: "Id_Cliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id_Ticket = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoPlaca = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id_Espacio = table.Column<int>(type: "int", nullable: false),
                    Fecha_hora_entrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fecha_hora_salida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id_Tarifa = table.Column<int>(type: "int", nullable: false),
                    PagoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id_Ticket);
                    table.ForeignKey(
                        name: "FK_Tickets_EspaciosEstacionamiento_Id_Espacio",
                        column: x => x.Id_Espacio,
                        principalTable: "EspaciosEstacionamiento",
                        principalColumn: "Id_Espacio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Tarifas_Id_Tarifa",
                        column: x => x.Id_Tarifa,
                        principalTable: "Tarifas",
                        principalColumn: "Id_Tarifa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Vehiculos_NoPlaca",
                        column: x => x.NoPlaca,
                        principalTable: "Vehiculos",
                        principalColumn: "NoPlaca",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    Id_Pago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Ticket = table.Column<int>(type: "int", nullable: false),
                    MontoPago = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MetodoPago = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoPago = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.Id_Pago);
                    table.ForeignKey(
                        name: "FK_Pagos_Tickets_Id_Ticket",
                        column: x => x.Id_Ticket,
                        principalTable: "Tickets",
                        principalColumn: "Id_Ticket",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_Id_Ticket",
                table: "Pagos",
                column: "Id_Ticket");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Id_Espacio",
                table: "Tickets",
                column: "Id_Espacio");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Id_Tarifa",
                table: "Tickets",
                column: "Id_Tarifa");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_NoPlaca",
                table: "Tickets",
                column: "NoPlaca");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_Id_Cliente",
                table: "Vehiculos",
                column: "Id_Cliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "EspaciosEstacionamiento");

            migrationBuilder.DropTable(
                name: "Tarifas");

            migrationBuilder.DropTable(
                name: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
