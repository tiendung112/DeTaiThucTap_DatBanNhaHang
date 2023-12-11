using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatBanNhaHang.Migrations
{
    /// <inheritdoc />
    public partial class nhaHang_v9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThoiGianTao",
                table: "HoaDon",
                newName: "ThoiGianKetThucThucTe");

            migrationBuilder.RenameColumn(
                name: "ThoiGianCapNhap",
                table: "HoaDon",
                newName: "ThoiGianHuyDat");

            migrationBuilder.AddColumn<string>(
                name: "SDT",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ThoiGianBatDauThucTe",
                table: "HoaDon",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ThoiGianDat",
                table: "HoaDon",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ThoiGianDuKienBatDau",
                table: "HoaDon",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ThoiGianDuKienKetThuc",
                table: "HoaDon",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "HinhAnhBanURL",
                table: "Ban",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mota",
                table: "Ban",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TinhTrangHienTai",
                table: "Ban",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SDT",
                table: "User");

            migrationBuilder.DropColumn(
                name: "address",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ThoiGianBatDauThucTe",
                table: "HoaDon");

            migrationBuilder.DropColumn(
                name: "ThoiGianDat",
                table: "HoaDon");

            migrationBuilder.DropColumn(
                name: "ThoiGianDuKienBatDau",
                table: "HoaDon");

            migrationBuilder.DropColumn(
                name: "ThoiGianDuKienKetThuc",
                table: "HoaDon");

            migrationBuilder.DropColumn(
                name: "HinhAnhBanURL",
                table: "Ban");

            migrationBuilder.DropColumn(
                name: "Mota",
                table: "Ban");

            migrationBuilder.DropColumn(
                name: "TinhTrangHienTai",
                table: "Ban");

            migrationBuilder.RenameColumn(
                name: "ThoiGianKetThucThucTe",
                table: "HoaDon",
                newName: "ThoiGianTao");

            migrationBuilder.RenameColumn(
                name: "ThoiGianHuyDat",
                table: "HoaDon",
                newName: "ThoiGianCapNhap");
        }
    }
}
