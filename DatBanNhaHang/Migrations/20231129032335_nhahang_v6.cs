using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatBanNhaHang.Migrations
{
    /// <inheritdoc />
    public partial class nhahang_v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeOrder");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeOrder",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BanID = table.Column<int>(type: "int", nullable: false),
                    HoaDonID = table.Column<int>(type: "int", nullable: false),
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
                name: "IX_TimeOrder_BanID",
                table: "TimeOrder",
                column: "BanID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeOrder_HoaDonID",
                table: "TimeOrder",
                column: "HoaDonID");
        }
    }
}
