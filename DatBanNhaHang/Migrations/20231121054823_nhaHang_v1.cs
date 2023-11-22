using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatBanNhaHang.Migrations
{
    /// <inheritdoc />
    public partial class nhaHang_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DauBep",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnhDauBepURl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ngaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DauBep", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiBan",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiBan = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiBan", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiMonAn",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiMonAn", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.id);
                });

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

            migrationBuilder.CreateTable(
                name: "TrangThaiHoaDon",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrangThaiHoaDon", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MonAn",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoaiMonAnID = table.Column<int>(type: "int", nullable: false),
                    TenMon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiaTien = table.Column<double>(type: "float", nullable: true),
                    AnhMonAn1URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnhMonAn2URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnhMonAn3URL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonAn", x => x.id);
                    table.ForeignKey(
                        name: "FK_MonAn_LoaiMonAn_LoaiMonAnID",
                        column: x => x.LoaiMonAnID,
                        principalTable: "LoaiMonAn",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ngayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Roleid = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK_User_Role_Roleid",
                        column: x => x.Roleid,
                        principalTable: "Role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ban",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViTri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoBan = table.Column<int>(type: "int", nullable: true),
                    SoNguoiToiDa = table.Column<int>(type: "int", nullable: true),
                    GiaTien = table.Column<double>(type: "float", nullable: true),
                    LoaiBanID = table.Column<int>(type: "int", nullable: true),
                    TrangThaiBanID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ban", x => x.id);
                    table.ForeignKey(
                        name: "FK_Ban_LoaiBan_LoaiBanID",
                        column: x => x.LoaiBanID,
                        principalTable: "LoaiBan",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Ban_TrangThaiBan_TrangThaiBanID",
                        column: x => x.TrangThaiBanID,
                        principalTable: "TrangThaiBan",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiredTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "XacNhanEmail",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ThoiGianHetHan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaXacNhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DaXacNhan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XacNhanEmail", x => x.id);
                    table.ForeignKey(
                        name: "FK_XacNhanEmail_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KhachHangID = table.Column<int>(type: "int", nullable: false),
                    BanID = table.Column<int>(type: "int", nullable: true),
                    TrangThaiHoaDonID = table.Column<int>(type: "int", nullable: true),
                    TenHoaDon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaGiaoDich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ThoiGianCapNhap = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongTien = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.id);
                    table.ForeignKey(
                        name: "FK_HoaDon_Ban_BanID",
                        column: x => x.BanID,
                        principalTable: "Ban",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_HoaDon_KhachHang_KhachHangID",
                        column: x => x.KhachHangID,
                        principalTable: "KhachHang",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoaDon_TrangThaiHoaDon_TrangThaiHoaDonID",
                        column: x => x.TrangThaiHoaDonID,
                        principalTable: "TrangThaiHoaDon",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietHoaDon",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoaDonID = table.Column<int>(type: "int", nullable: false),
                    MonAnID = table.Column<int>(type: "int", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: true),
                    DonViTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
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

            migrationBuilder.CreateIndex(
                name: "IX_Ban_LoaiBanID",
                table: "Ban",
                column: "LoaiBanID");

            migrationBuilder.CreateIndex(
                name: "IX_Ban_TrangThaiBanID",
                table: "Ban",
                column: "TrangThaiBanID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDon_HoaDonID",
                table: "ChiTietHoaDon",
                column: "HoaDonID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDon_MonAnID",
                table: "ChiTietHoaDon",
                column: "MonAnID");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_BanID",
                table: "HoaDon",
                column: "BanID");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_KhachHangID",
                table: "HoaDon",
                column: "KhachHangID");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_TrangThaiHoaDonID",
                table: "HoaDon",
                column: "TrangThaiHoaDonID");

            migrationBuilder.CreateIndex(
                name: "IX_MonAn_LoaiMonAnID",
                table: "MonAn",
                column: "LoaiMonAnID");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserID",
                table: "RefreshToken",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Roleid",
                table: "User",
                column: "Roleid");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "User",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_XacNhanEmail_UserID",
                table: "XacNhanEmail",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietHoaDon");

            migrationBuilder.DropTable(
                name: "DauBep");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "XacNhanEmail");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "MonAn");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Ban");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "TrangThaiHoaDon");

            migrationBuilder.DropTable(
                name: "LoaiMonAn");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "LoaiBan");

            migrationBuilder.DropTable(
                name: "TrangThaiBan");
        }
    }
}
