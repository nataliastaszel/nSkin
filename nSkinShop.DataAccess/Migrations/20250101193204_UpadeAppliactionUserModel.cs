using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nSkinShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpadeAppliactionUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "AspNetUsers",
                newName: "PostalCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "AspNetUsers",
                newName: "ZipCode");
        }
    }
}
