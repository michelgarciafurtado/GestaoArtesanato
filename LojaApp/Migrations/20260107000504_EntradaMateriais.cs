using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaApp.Migrations
{
    /// <inheritdoc />
    public partial class EntradaMateriais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entradas",
                columns: table => new
                {
                    IdEntrada = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataEntrada = table.Column<DateOnly>(type: "date", nullable: false),
                    IdSubstancia = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    PesoTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TipoMedida = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    ValorUn = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entradas", x => x.IdEntrada);
                    table.ForeignKey(
                        name: "FK_Entradas_Substancias_IdSubstancia",
                        column: x => x.IdSubstancia,
                        principalTable: "Substancias",
                        principalColumn: "IdSubstancia",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_IdSubstancia",
                table: "Entradas",
                column: "IdSubstancia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entradas");
        }
    }
}
