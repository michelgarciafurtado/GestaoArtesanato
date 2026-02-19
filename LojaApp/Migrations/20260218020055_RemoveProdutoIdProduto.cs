using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaApp.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProdutoIdProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagensProdutos_Produtos_ProdutoIdProduto",
                table: "ImagensProdutos");

            migrationBuilder.DropIndex(
                name: "IX_ImagensProdutos_ProdutoIdProduto",
                table: "ImagensProdutos");

            migrationBuilder.DropColumn(
                name: "ProdutoIdProduto",
                table: "ImagensProdutos");

            migrationBuilder.CreateIndex(
                name: "IX_ImagensProdutos_IdProduto",
                table: "ImagensProdutos",
                column: "IdProduto");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagensProdutos_Produtos_IdProduto",
                table: "ImagensProdutos",
                column: "IdProduto",
                principalTable: "Produtos",
                principalColumn: "IdProduto",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagensProdutos_Produtos_IdProduto",
                table: "ImagensProdutos");

            migrationBuilder.DropIndex(
                name: "IX_ImagensProdutos_IdProduto",
                table: "ImagensProdutos");

            migrationBuilder.AddColumn<string>(
                name: "ProdutoIdProduto",
                table: "ImagensProdutos",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImagensProdutos_ProdutoIdProduto",
                table: "ImagensProdutos",
                column: "ProdutoIdProduto");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagensProdutos_Produtos_ProdutoIdProduto",
                table: "ImagensProdutos",
                column: "ProdutoIdProduto",
                principalTable: "Produtos",
                principalColumn: "IdProduto");
        }
    }
}
