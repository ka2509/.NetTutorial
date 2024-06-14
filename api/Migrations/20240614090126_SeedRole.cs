using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class SeedRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "786b40e6-d238-4982-a62f-9fd219237deb", "2225b1d6-e5bb-4e18-b8de-ab60dcdcb469", "Admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "89e6a8e2-311a-4103-a1e7-b2bbfaa70673", "3dc118d3-6588-46f9-87e7-3c6c801b6db9", "User", "user" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "786b40e6-d238-4982-a62f-9fd219237deb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89e6a8e2-311a-4103-a1e7-b2bbfaa70673");
        }
    }
}
