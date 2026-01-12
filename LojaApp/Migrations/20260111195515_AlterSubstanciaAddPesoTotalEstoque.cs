using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaApp.Migrations
{
    /// <inheritdoc />
    public partial class AlterSubstanciaAddPesoTotalEstoque : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PesoTotal",
                table: "Entradas",
                newName: "PesoUn");

            migrationBuilder.AddColumn<decimal>(
                name: "PesoTotalEstoque",
                table: "Substancias",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PesoTotalEstoque",
                table: "Substancias");

            migrationBuilder.RenameColumn(
                name: "PesoUn",
                table: "Entradas",
                newName: "PesoTotal");
        }
    }
}
