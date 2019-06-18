using Microsoft.EntityFrameworkCore.Migrations;

namespace EExpress.Migrations
{
    public partial class basketUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ProductState",
                table: "Basket",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductState",
                table: "Basket");
        }
    }
}
