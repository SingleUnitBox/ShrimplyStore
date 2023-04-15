using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shrimply.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Shrimp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Shrimps",
                columns: new[] { "Id", "BarCode", "Description", "ListPrice", "Name", "Owner", "Price", "Price100", "Price50" },
                values: new object[,]
                {
                    { 1, "12345", "PRL", 0.0, "Pure Red Line", "Cez", 0.0, 0.0, 0.0 },
                    { 2, "12345", "PBL", 0.0, "Pure Black Line", "Zuk", 0.0, 0.0, 0.0 },
                    { 3, "12345", "PWL", 0.0, "Pure White Line", "Zek", 0.0, 0.0, 0.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Shrimps",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Shrimps",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Shrimps",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
