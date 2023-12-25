using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatBanNhaHang.Migrations
{
    /// <inheritdoc />
    public partial class nhaHang_V14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoaDon_KhachHang_KhachHangID",
                table: "HoaDon");

            migrationBuilder.DropTable(
                name: "ChiTietHoaDon");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropColumn(
                name: "TinhTrangHienTai",
                table: "Ban");

            migrationBuilder.RenameColumn(
                name: "KhachHangID",
                table: "HoaDon",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_HoaDon_KhachHangID",
                table: "HoaDon",
                newName: "IX_HoaDon_userId");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "XacNhanEmail",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "TrangThaiHoaDon",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "Role",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "RefreshToken",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "NhanXet",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "MonAn",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "LoaiMonAn",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "LoaiBan",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "LienHe",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "HoaDon",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "DauBep",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "Ban",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "BaiViet",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "Admin",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDon_User_userId",
                table: "HoaDon",
                column: "userId",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoaDon_User_userId",
                table: "HoaDon");

            migrationBuilder.DropColumn(
                name: "status",
                table: "XacNhanEmail");

            migrationBuilder.DropColumn(
                name: "status",
                table: "User");

            migrationBuilder.DropColumn(
                name: "status",
                table: "TrangThaiHoaDon");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "status",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "status",
                table: "NhanXet");

            migrationBuilder.DropColumn(
                name: "status",
                table: "MonAn");

            migrationBuilder.DropColumn(
                name: "status",
                table: "LoaiMonAn");

            migrationBuilder.DropColumn(
                name: "status",
                table: "LoaiBan");

            migrationBuilder.DropColumn(
                name: "status",
                table: "LienHe");

            migrationBuilder.DropColumn(
                name: "status",
                table: "HoaDon");

            migrationBuilder.DropColumn(
                name: "status",
                table: "DauBep");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Ban");

            migrationBuilder.DropColumn(
                name: "status",
                table: "BaiViet");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Admin");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "HoaDon",
                newName: "KhachHangID");

            migrationBuilder.RenameIndex(
                name: "IX_HoaDon_userId",
                table: "HoaDon",
                newName: "IX_HoaDon_KhachHangID");

            migrationBuilder.AddColumn<string>(
                name: "TinhTrangHienTai",
                table: "Ban",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChiTietHoaDon",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoaDonID = table.Column<int>(type: "int", nullable: false),
                    MonAnID = table.Column<int>(type: "int", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: true),
                    ThanhTien = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietHoaDon", x => x.id);
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDon_HoaDon_HoaDonID",
                        column: x => x.HoaDonID,
                        principalTable: "HoaDon",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDon_MonAn_MonAnID",
                        column: x => x.MonAnID,
                        principalTable: "MonAn",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(type: "int", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.id);
                    table.ForeignKey(
                        name: "FK_KhachHang_User_userID",
                        column: x => x.userID,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDon_HoaDonID",
                table: "ChiTietHoaDon",
                column: "HoaDonID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDon_MonAnID",
                table: "ChiTietHoaDon",
                column: "MonAnID");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHang_userID",
                table: "KhachHang",
                column: "userID");

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDon_KhachHang_KhachHangID",
                table: "HoaDon",
                column: "KhachHangID",
                principalTable: "KhachHang",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
