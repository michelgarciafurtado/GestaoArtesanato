using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    IdCategoria = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.IdCategoria);
                });

            migrationBuilder.CreateTable(
                name: "MateriasPrimas",
                columns: table => new
                {
                    IdMateriaPrima = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    VlUn = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TpMedida = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriasPrimas", x => x.IdMateriaPrima);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    IdProduto = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NomeProduto = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IdCategoria = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PesoProduto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    medidaEnum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlImg = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.IdProduto);
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categorias",
                        principalColumn: "IdCategoria");
                });

            migrationBuilder.CreateTable(
                name: "Ingredientes",
                columns: table => new
                {
                    IdIngrediente = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IdMateriaPrima = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    MateriaPrimaIdMateriaPrima = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IdProduto = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ProdutoIdProduto = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    QtdIngrediente = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredientes", x => x.IdIngrediente);
                    table.ForeignKey(
                        name: "FK_Ingredientes_Produtos_ProdutoIdProduto",
                        column: x => x.ProdutoIdProduto,
                        principalTable: "Produtos",
                        principalColumn: "IdProduto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingredientes_MateriasPrimas_MateriaPrimaIdMateriaPrima",
                        column: x => x.MateriaPrimaIdMateriaPrima,
                        principalTable: "MateriasPrimas",
                        principalColumn: "IdMateriaPrima",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredientes_ProdutoIdProduto",
                table: "Ingredientes",
                column: "ProdutoIdProduto");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredientes_MateriaPrimaIdMateriaPrima",
                table: "Ingredientes",
                column: "MateriaPrimaIdMateriaPrima");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_IdCategoria",
                table: "Produtos",
                column: "IdCategoria");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredientes");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "MateriasPrimas");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
