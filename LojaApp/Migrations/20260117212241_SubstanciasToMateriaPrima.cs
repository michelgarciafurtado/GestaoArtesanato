using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaApp.Migrations
{
    /// <inheritdoc />
    public partial class SubstanciasToMateriaPrima : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entradas_MateriasPrimas_IdMateriaPrima",
                table: "Entradas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredientes_MateriasPrimas_IdMateriaPrima",
                table: "Ingredientes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MateriasPrimas",
                table: "MateriasPrimas");

            migrationBuilder.RenameTable(
                name: "MateriasPrimas",
                newName: "MateriasPrimas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MateriasPrimas",
                table: "MateriasPrimas",
                column: "IdMateriaPrima");

            migrationBuilder.AddForeignKey(
                name: "FK_Entradas_MateriasPrimas_IdMateriaPrima",
                table: "Entradas",
                column: "IdMateriaPrima",
                principalTable: "MateriasPrimas",
                principalColumn: "IdMateriaPrima",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredientes_MateriasPrimas_IdMateriaPrima",
                table: "Ingredientes",
                column: "IdMateriaPrima",
                principalTable: "MateriasPrimas",
                principalColumn: "IdMateriaPrima",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entradas_MateriasPrimas_IdMateriaPrima",
                table: "Entradas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredientes_MateriasPrimas_IdMateriaPrima",
                table: "Ingredientes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MateriasPrimas",
                table: "MateriasPrimas");

            migrationBuilder.RenameTable(
                name: "MateriasPrimas",
                newName: "MateriasPrimas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MateriasPrimas",
                table: "MateriasPrimas",
                column: "IdMateriaPrima");

            migrationBuilder.AddForeignKey(
                name: "FK_Entradas_MateriasPrimas_IdMateriaPrima",
                table: "Entradas",
                column: "IdMateriaPrima",
                principalTable: "MateriasPrimas",
                principalColumn: "IdMateriaPrima",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredientes_MateriasPrimas_IdMateriaPrima",
                table: "Ingredientes",
                column: "IdMateriaPrima",
                principalTable: "MateriasPrimas",
                principalColumn: "IdMateriaPrima",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
