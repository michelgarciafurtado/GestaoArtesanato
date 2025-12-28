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
                name: "FK_Ingredientes_Substancias_SubstanciaIdSubstancia",
                table: "Ingredientes");

            migrationBuilder.DropIndex(
                name: "IX_Ingredientes_ProdutoIdProduto",
                table: "Ingredientes");

            migrationBuilder.DropIndex(
                name: "IX_Ingredientes_SubstanciaIdSubstancia",
                table: "Ingredientes");

            migrationBuilder.DropColumn(
                name: "ProdutoIdProduto",
                table: "Ingredientes");

            migrationBuilder.DropColumn(
                name: "SubstanciaIdSubstancia",
                table: "Ingredientes");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredientes_IdProduto",
                table: "Ingredientes",
                column: "IdProduto");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredientes_IdSubstancia",
                table: "Ingredientes",
                column: "IdSubstancia");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredientes_Produtos_IdProduto",
                table: "Ingredientes",
                column: "IdProduto",
                principalTable: "Produtos",
                principalColumn: "IdProduto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredientes_Substancias_IdSubstancia",
                table: "Ingredientes",
                column: "IdSubstancia",
                principalTable: "Substancias",
                principalColumn: "IdSubstancia",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredientes_Produtos_IdProduto",
                table: "Ingredientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredientes_Substancias_IdSubstancia",
                table: "Ingredientes");

            migrationBuilder.DropIndex(
                name: "IX_Ingredientes_IdProduto",
                table: "Ingredientes");

            migrationBuilder.DropIndex(
                name: "IX_Ingredientes_IdSubstancia",
                table: "Ingredientes");

            migrationBuilder.AddColumn<string>(
                name: "ProdutoIdProduto",
                table: "Ingredientes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubstanciaIdSubstancia",
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
                name: "IX_Ingredientes_SubstanciaIdSubstancia",
                table: "Ingredientes",
                column: "SubstanciaIdSubstancia");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredientes_Produtos_ProdutoIdProduto",
                table: "Ingredientes",
                column: "ProdutoIdProduto",
                principalTable: "Produtos",
                principalColumn: "IdProduto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredientes_Substancias_SubstanciaIdSubstancia",
                table: "Ingredientes",
                column: "SubstanciaIdSubstancia",
                principalTable: "Substancias",
                principalColumn: "IdSubstancia",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
