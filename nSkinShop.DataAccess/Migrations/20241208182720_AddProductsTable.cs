using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace nSkinShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddProductsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price100ml = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Description", "Price", "Price100ml", "Title" },
                values: new object[,]
                {
                    { 1, "Drunk Elephant", "Moisturizing face cream", 85.530000000000001, 116.27, "Protini Polypeptide Cream" },
                    { 2, "Clinique", "Nourishing night cream", 100.89, 205.77000000000001, "Smart Clinical Repair Wrinkle Correcting Cream" },
                    { 3, "Sol de Janeiro", "Brazilian body cream", 23.550000000000001, 31.34, "Brazilian Bum Bum Cream" },
                    { 4, "Rituals", "Shower gel foam", 13.550000000000001, 17.890000000000001, "The Ritual of Ayurveda" },
                    { 5, "SuperGoop", "Sun protection filter SPF 30", 24.539999999999999, 50.0, "Glowscreen" },
                    { 6, "Clarins", "After sun care", 34.460000000000001, 76.549999999999997, "Soothing After Sun Balm" },
                    { 7, "Dr Irena Eris", "Smoothing peeling", 34.460000000000001, 80.060000000000002, "Body Art Smoothing Body Scrub with Alabaster " },
                    { 8, "Phlov", "Intensive balm against stretch marks", 27.02, 54.82, "Take control, Babe!" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);
        }
    }
}
