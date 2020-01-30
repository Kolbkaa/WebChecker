using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebChecker.Migrations
{
    public partial class PriceClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ProductEntity");

            migrationBuilder.DropColumn(
                name: "Test",
                table: "ProductEntity");

            migrationBuilder.AddColumn<int>(
                name: "PriceId",
                table: "ProductEntity",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Price",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Unity = table.Column<int>(nullable: true),
                    Dec = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Price", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntity_PriceId",
                table: "ProductEntity",
                column: "PriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductEntity_Price_PriceId",
                table: "ProductEntity",
                column: "PriceId",
                principalTable: "Price",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductEntity_Price_PriceId",
                table: "ProductEntity");

            migrationBuilder.DropTable(
                name: "Price");

            migrationBuilder.DropIndex(
                name: "IX_ProductEntity_PriceId",
                table: "ProductEntity");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "ProductEntity");

            migrationBuilder.AddColumn<string>(
                name: "Price",
                table: "ProductEntity",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Test",
                table: "ProductEntity",
                nullable: true);
        }
    }
}
