using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaApp.Migrations
{
    /// <inheritdoc />
    public partial class CalcularPrecoProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MargemLucro",
                table: "Produtos",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Custos",
                columns: table => new
                {
                    IdCusto = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DescricaoCusto = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ValorCusto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IdProduto = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Custos", x => x.IdCusto);
                    table.ForeignKey(
                        name: "FK_Custos_Produtos_IdProduto",
                        column: x => x.IdProduto,
                        principalTable: "Produtos",
                        principalColumn: "IdProduto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Custos_IdProduto",
                table: "Custos",
                column: "IdProduto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Custos");

            migrationBuilder.DropColumn(
                name: "MargemLucro",
                table: "Produtos");
        }
    }
}
