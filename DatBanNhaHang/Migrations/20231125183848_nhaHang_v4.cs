using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatBanNhaHang.Migrations
{
    /// <inheritdoc />
    public partial class nhaHang_v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Admin_Adminid",
                table: "RefreshToken");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_User_UserID",
                table: "RefreshToken");

            migrationBuilder.DropForeignKey(
                name: "FK_XacNhanEmail_Admin_Adminid",
                table: "XacNhanEmail");

            migrationBuilder.DropForeignKey(
                name: "FK_XacNhanEmail_User_UserID",
                table: "XacNhanEmail");

            migrationBuilder.RenameColumn(
                name: "Adminid",
                table: "XacNhanEmail",
                newName: "AdminID");

            migrationBuilder.RenameIndex(
                name: "IX_XacNhanEmail_Adminid",
                table: "XacNhanEmail",
                newName: "IX_XacNhanEmail_AdminID");

            migrationBuilder.RenameColumn(
                name: "Adminid",
                table: "RefreshToken",
                newName: "AdminID");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_Adminid",
                table: "RefreshToken",
                newName: "IX_RefreshToken_AdminID");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "XacNhanEmail",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "RefreshToken",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Admin_AdminID",
                table: "RefreshToken",
                column: "AdminID",
                principalTable: "Admin",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_User_UserID",
                table: "RefreshToken",
                column: "UserID",
                principalTable: "User",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_XacNhanEmail_Admin_AdminID",
                table: "XacNhanEmail",
                column: "AdminID",
                principalTable: "Admin",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_XacNhanEmail_User_UserID",
                table: "XacNhanEmail",
                column: "UserID",
                principalTable: "User",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Admin_AdminID",
                table: "RefreshToken");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_User_UserID",
                table: "RefreshToken");

            migrationBuilder.DropForeignKey(
                name: "FK_XacNhanEmail_Admin_AdminID",
                table: "XacNhanEmail");

            migrationBuilder.DropForeignKey(
                name: "FK_XacNhanEmail_User_UserID",
                table: "XacNhanEmail");

            migrationBuilder.RenameColumn(
                name: "AdminID",
                table: "XacNhanEmail",
                newName: "Adminid");

            migrationBuilder.RenameIndex(
                name: "IX_XacNhanEmail_AdminID",
                table: "XacNhanEmail",
                newName: "IX_XacNhanEmail_Adminid");

            migrationBuilder.RenameColumn(
                name: "AdminID",
                table: "RefreshToken",
                newName: "Adminid");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_AdminID",
                table: "RefreshToken",
                newName: "IX_RefreshToken_Adminid");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "XacNhanEmail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "RefreshToken",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Admin_Adminid",
                table: "RefreshToken",
                column: "Adminid",
                principalTable: "Admin",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_User_UserID",
                table: "RefreshToken",
                column: "UserID",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_XacNhanEmail_Admin_Adminid",
                table: "XacNhanEmail",
                column: "Adminid",
                principalTable: "Admin",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_XacNhanEmail_User_UserID",
                table: "XacNhanEmail",
                column: "UserID",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
