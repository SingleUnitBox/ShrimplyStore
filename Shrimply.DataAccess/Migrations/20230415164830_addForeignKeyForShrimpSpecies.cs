using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shrimply.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addForeignKeyForShrimpSpecies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shrimps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false),
                    SpeciesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shrimps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shrimps_Species_SpeciesId",
                        column: x => x.SpeciesId,
                        principalTable: "Species",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Species",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Caridina" },
                    { 2, 2, "Neocaridina" },
                    { 3, 3, "Sulawesi" }
                });

            migrationBuilder.InsertData(
                table: "Shrimps",
                columns: new[] { "Id", "BarCode", "Description", "ListPrice", "Name", "Owner", "Price", "Price100", "Price50", "SpeciesId" },
                values: new object[,]
                {
                    { 1, "12345", "PRL", 10.0, "Pure Red Line", "Cez", 5.0, 3.0, 4.0, 1 },
                    { 2, "12345", "PBL", 16.0, "Pure Black Line", "Zuk", 12.0, 8.0, 10.0, 1 },
                    { 3, "12345", "PWL", 10.0, "Pure White Line", "Zek", 5.0, 3.0, 4.0, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shrimps_SpeciesId",
                table: "Shrimps",
                column: "SpeciesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shrimps");

            migrationBuilder.DropTable(
                name: "Species");
        }
    }
}
