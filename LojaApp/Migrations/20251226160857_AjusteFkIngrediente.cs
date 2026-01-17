using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaApp.Migrations
{
    /// <inheritdoc />
    public partial class AjusteFkIngrediente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredientes_Produtos_ProdutoIdProduto",
                table: "Ingredientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredientes_MateriasPrimas_MateriaPrimaIdMateriaPrima",
                table: "Ingredientes");

            migrationBuilder.DropIndex(
                name: "IX_Ingredientes_ProdutoIdProduto",
                table: "Ingredientes");

            migrationBuilder.DropIndex(
                name: "IX_Ingredientes_MateriaPrimaIdMateriaPrima",
                table: "Ingredientes");

            migrationBuilder.DropColumn(
                name: "ProdutoIdProduto",
                table: "Ingredientes");

            migrationBuilder.DropColumn(
                name: "MateriaPrimaIdMateriaPrima",
                table: "Ingredientes");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredientes_IdProduto",
                table: "Ingredientes",
                column: "IdProduto");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredientes_IdMateriaPrima",
                table: "Ingredientes",
                column: "IdMateriaPrima");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredientes_Produtos_IdProduto",
                table: "Ingredientes",
                column: "IdProduto",
                principalTable: "Produtos",
                principalColumn: "IdProduto",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Ingredientes_Produtos_IdProduto",
                table: "Ingredientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredientes_MateriasPrimas_IdMateriaPrima",
                table: "Ingredientes");

            migrationBuilder.DropIndex(
                name: "IX_Ingredientes_IdProduto",
                table: "Ingredientes");

            migrationBuilder.DropIndex(
                name: "IX_Ingredientes_IdMateriaPrima",
                table: "Ingredientes");

            migrationBuilder.AddColumn<string>(
                name: "ProdutoIdProduto",
                table: "Ingredientes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MateriaPrimaIdMateriaPrima",
                table: "Ingredientes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredientes_ProdutoIdProduto",
                table: "Ingredientes",
                column: "ProdutoIdProduto");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredientes_MateriaPrimaIdMateriaPrima",
                table: "Ingredientes",
                column: "MateriaPrimaIdMateriaPrima");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredientes_Produtos_ProdutoIdProduto",
                table: "Ingredientes",
                column: "ProdutoIdProduto",
                principalTable: "Produtos",
                principalColumn: "IdProduto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredientes_MateriasPrimas_MateriaPrimaIdMateriaPrima",
                table: "Ingredientes",
                column: "MateriaPrimaIdMateriaPrima",
                principalTable: "MateriasPrimas",
                principalColumn: "IdMateriaPrima",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
