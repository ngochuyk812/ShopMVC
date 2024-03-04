using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopMVC.Migrations
{
    /// <inheritdoc />
    public partial class Update_Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ImportProducts");

            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Products",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "Products",
                type: "varchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "Products");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "ImportProducts",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
