using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace laboratorio1ElvisOrtiz160625.Migrations
{
    /// <inheritdoc />
    public partial class departamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartamentoId",
                table: "Municipios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "tblDepartamento",
                columns: table => new
                {
                    DepartamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Inactivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDepartamento", x => x.DepartamentoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Municipios_DepartamentoId",
                table: "Municipios",
                column: "DepartamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Municipios_tblDepartamento_DepartamentoId",
                table: "Municipios",
                column: "DepartamentoId",
                principalTable: "tblDepartamento",
                principalColumn: "DepartamentoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Municipios_tblDepartamento_DepartamentoId",
                table: "Municipios");

            migrationBuilder.DropTable(
                name: "tblDepartamento");

            migrationBuilder.DropIndex(
                name: "IX_Municipios_DepartamentoId",
                table: "Municipios");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "Municipios");
        }
    }
}
