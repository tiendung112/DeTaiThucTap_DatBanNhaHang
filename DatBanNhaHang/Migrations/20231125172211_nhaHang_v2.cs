using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatBanNhaHang.Migrations
{
    /// <inheritdoc />
    public partial class nhaHang_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ban_TrangThaiBan_TrangThaiBanID",
                table: "Ban");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_Roleid",
                table: "User");

            migrationBuilder.DropTable(
                name: "TrangThaiBan");

            migrationBuilder.DropIndex(
                name: "IX_Ban_TrangThaiBanID",
                table: "Ban");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TrangThaiBanID",
                table: "Ban");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "User",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "Adminid",
                table: "XacNhanEmail",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Roleid",
                table: "User",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Adminid",
                table: "RefreshToken",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiBan",
                table: "Ban",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ngaysinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    QueQuan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RoleID = table.Column<int>(type: "int", nullable: true),
                    ParentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.id);
                    table.ForeignKey(
                        name: "FK_Admin_Admin_ParentID",
                        column: x => x.ParentID,
                        principalTable: "Admin",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Admin_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "TimeOrder",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoaDonID = table.Column<int>(type: "int", nullable: false),
                    BanID = table.Column<int>(type: "int", nullable: false),
                    timeOrderIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    timeOrderOut = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeOrder", x => x.id);
                    table.ForeignKey(
                        name: "FK_TimeOrder_Ban_BanID",
                        column: x => x.BanID,
                        principalTable: "Ban",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeOrder_HoaDon_HoaDonID",
                        column: x => x.HoaDonID,
                        principalTable: "HoaDon",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_XacNhanEmail_Adminid",
                table: "XacNhanEmail",
                column: "Adminid");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_Adminid",
                table: "RefreshToken",
                column: "Adminid");

            migrationBuilder.CreateIndex(
                name: "IX_Admin_AdminName",
                table: "Admin",
                column: "AdminName",
                unique: true,
                filter: "[AdminName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Admin_ParentID",
                table: "Admin",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_Admin_RoleID",
                table: "Admin",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeOrder_BanID",
                table: "TimeOrder",
                column: "BanID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeOrder_HoaDonID",
                table: "TimeOrder",
                column: "HoaDonID");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Admin_Adminid",
                table: "RefreshToken",
                column: "Adminid",
                principalTable: "Admin",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_Roleid",
                table: "User",
                column: "Roleid",
                principalTable: "Role",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_XacNhanEmail_Admin_Adminid",
                table: "XacNhanEmail",
                column: "Adminid",
                principalTable: "Admin",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Admin_Adminid",
                table: "RefreshToken");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_Roleid",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_XacNhanEmail_Admin_Adminid",
                table: "XacNhanEmail");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "TimeOrder");

            migrationBuilder.DropIndex(
                name: "IX_XacNhanEmail_Adminid",
                table: "XacNhanEmail");

            migrationBuilder.DropIndex(
                name: "IX_RefreshToken_Adminid",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "Adminid",
                table: "XacNhanEmail");

            migrationBuilder.DropColumn(
                name: "Adminid",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "TrangThaiBan",
                table: "Ban");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "User",
                newName: "LastName");

            migrationBuilder.AlterColumn<int>(
                name: "Roleid",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TrangThaiBanID",
                table: "Ban",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TrangThaiBan",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrangThaiBan", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ban_TrangThaiBanID",
                table: "Ban",
                column: "TrangThaiBanID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ban_TrangThaiBan_TrangThaiBanID",
                table: "Ban",
                column: "TrangThaiBanID",
                principalTable: "TrangThaiBan",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_Roleid",
                table: "User",
                column: "Roleid",
                principalTable: "Role",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
