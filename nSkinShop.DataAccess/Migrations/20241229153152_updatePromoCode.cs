using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nSkinShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updatePromoCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "DiscountAmount",
                table: "PromoCodes",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountAmount",
                table: "PromoCodes",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
