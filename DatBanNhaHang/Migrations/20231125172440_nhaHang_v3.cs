using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatBanNhaHang.Migrations
{
    /// <inheritdoc />
    public partial class nhaHang_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_Roleid",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_Roleid",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Roleid",
                table: "User");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Roleid",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Roleid",
                table: "User",
                column: "Roleid");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_Roleid",
                table: "User",
                column: "Roleid",
                principalTable: "Role",
                principalColumn: "id");
        }
    }
}
