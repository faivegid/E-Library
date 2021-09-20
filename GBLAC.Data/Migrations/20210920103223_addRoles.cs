using Microsoft.EntityFrameworkCore.Migrations;

namespace GBLAC.Data.Migrations
{
    public partial class addRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d03c9762-89ea-4ea9-b79b-038ebe26e811", "397e9670-cb89-407f-9f46-7b119b8377d4", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "365a3c9c-4a7f-46b9-a932-bd687517c58b", "a6ecc389-6fd9-45c7-ae94-5705efb35b4a", "customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "365a3c9c-4a7f-46b9-a932-bd687517c58b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d03c9762-89ea-4ea9-b79b-038ebe26e811");
        }
    }
}
