using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class ConfigureCascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Stock_StockId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Stock_StockId",
                table: "Comments",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Stock_StockId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Stock_StockId",
                table: "Comments",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "Id");
        }
    }
}
