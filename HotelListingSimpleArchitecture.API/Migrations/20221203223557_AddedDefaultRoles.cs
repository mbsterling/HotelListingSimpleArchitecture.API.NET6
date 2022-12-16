using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelListing.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedDefaultRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7214da94-54cf-433e-9bf6-ed0cd307e846", "a8bb49de-24c1-4116-b248-50adca96db3a", "User", "USER" },
                    { "f4e9fa1d-8e9a-4958-aa1d-8ff6356b1594", "271bbcd4-37c5-4091-a473-3a37821f89d4", "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7214da94-54cf-433e-9bf6-ed0cd307e846");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4e9fa1d-8e9a-4958-aa1d-8ff6356b1594");
        }
    }
}
