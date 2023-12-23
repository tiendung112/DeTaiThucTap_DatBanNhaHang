using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatBanNhaHang.Migrations
{
    /// <inheritdoc />
    public partial class nhaHang_v13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnhMonAn2URL",
                table: "MonAn");

            migrationBuilder.DropColumn(
                name: "AnhMonAn3URL",
                table: "MonAn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnhMonAn2URL",
                table: "MonAn",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnhMonAn3URL",
                table: "MonAn",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
