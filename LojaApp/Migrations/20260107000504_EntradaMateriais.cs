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
                    IdMateriaPrima = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    PesoTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TipoMedida = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    ValorUn = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entradas", x => x.IdEntrada);
                    table.ForeignKey(
                        name: "FK_Entradas_MateriasPrimas_IdMateriaPrima",
                        column: x => x.IdMateriaPrima,
                        principalTable: "MateriasPrimas",
                        principalColumn: "IdMateriaPrima",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_IdMateriaPrima",
                table: "Entradas",
                column: "IdMateriaPrima");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entradas");
        }
    }
}
