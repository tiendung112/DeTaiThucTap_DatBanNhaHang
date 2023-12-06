using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatBanNhaHang.Migrations
{
    /// <inheritdoc />
    public partial class nhaHang_v7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userID",
                table: "KhachHang",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_KhachHang_userID",
                table: "KhachHang",
                column: "userID");

            migrationBuilder.AddForeignKey(
                name: "FK_KhachHang_User_userID",
                table: "KhachHang",
                column: "userID",
                principalTable: "User",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KhachHang_User_userID",
                table: "KhachHang");

            migrationBuilder.DropIndex(
                name: "IX_KhachHang_userID",
                table: "KhachHang");

            migrationBuilder.DropColumn(
                name: "userID",
                table: "KhachHang");
        }
    }
}
