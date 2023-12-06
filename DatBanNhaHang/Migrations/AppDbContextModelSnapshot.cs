﻿// <auto-generated />
using System;
using DatBanNhaHang.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DatBanNhaHang.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DatBanNhaHang.Entities.NguoiDung.Admin", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("AdminName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentID")
                        .HasColumnType("int");

                    b.Property<string>("QueQuan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleID")
                        .HasColumnType("int");

                    b.Property<string>("SDT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("create_at")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ngaysinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("AdminName")
                        .IsUnique()
                        .HasFilter("[AdminName] IS NOT NULL");

                    b.HasIndex("ParentID");

                    b.HasIndex("RoleID");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NguoiDung.RefreshToken", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("AdminID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpiredTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("AdminID");

                    b.HasIndex("UserID");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NguoiDung.Role", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NguoiDung.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("ngayTao")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NguoiDung.XacNhanEmail", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("AdminID")
                        .HasColumnType("int");

                    b.Property<bool>("DaXacNhan")
                        .HasColumnType("bit");

                    b.Property<string>("MaXacNhan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ThoiGianHetHan")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("AdminID");

                    b.HasIndex("UserID");

                    b.ToTable("XacNhanEmail");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NhaHang.Ban", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<double?>("GiaTien")
                        .HasColumnType("float");

                    b.Property<int?>("LoaiBanID")
                        .HasColumnType("int");

                    b.Property<int?>("SoBan")
                        .HasColumnType("int");

                    b.Property<int?>("SoNguoiToiDa")
                        .HasColumnType("int");

                    b.Property<string>("TrangThaiBan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ViTri")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("LoaiBanID");

                    b.ToTable("Ban");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NhaHang.ChiTietHoaDon", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("DonViTinh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HoaDonID")
                        .HasColumnType("int");

                    b.Property<int?>("MonAnID")
                        .HasColumnType("int");

                    b.Property<int?>("SoLuong")
                        .HasColumnType("int");

                    b.Property<double?>("ThanhTien")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.HasIndex("HoaDonID");

                    b.HasIndex("MonAnID");

                    b.ToTable("ChiTietHoaDon");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NhaHang.DauBep", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("AnhDauBepURl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoTen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SDT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ngaySinh")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.ToTable("DauBep");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NhaHang.HoaDon", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("BanID")
                        .HasColumnType("int");

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KhachHangID")
                        .HasColumnType("int");

                    b.Property<string>("MaGiaoDich")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenHoaDon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ThoiGianCapNhap")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ThoiGianTao")
                        .HasColumnType("datetime2");

                    b.Property<double?>("TongTien")
                        .HasColumnType("float");

                    b.Property<int?>("TrangThaiHoaDonID")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("BanID");

                    b.HasIndex("KhachHangID");

                    b.HasIndex("TrangThaiHoaDonID");

                    b.ToTable("HoaDon");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NhaHang.KhachHang", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoTen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("SDT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("userID")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("userID");

                    b.ToTable("KhachHang");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NhaHang.LoaiBan", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("TenLoaiBan")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("LoaiBan");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NhaHang.LoaiMonAn", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("TenLoai")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("LoaiMonAn");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NhaHang.MonAn", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("AnhMonAn1URL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AnhMonAn2URL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AnhMonAn3URL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("GiaTien")
                        .HasColumnType("float");

                    b.Property<int>("LoaiMonAnID")
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenMon")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("LoaiMonAnID");

                    b.ToTable("MonAn");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NhaHang.TrangThaiHoaDon", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("TenTrangThai")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("TrangThaiHoaDon");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NguoiDung.Admin", b =>
                {
                    b.HasOne("DatBanNhaHang.Entities.NguoiDung.Admin", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentID");

                    b.HasOne("DatBanNhaHang.Entities.NguoiDung.Role", "Role")
                        .WithMany("Admin")
                        .HasForeignKey("RoleID");

                    b.Navigation("Parent");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NguoiDung.RefreshToken", b =>
                {
                    b.HasOne("DatBanNhaHang.Entities.NguoiDung.Admin", "Admin")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("AdminID");

                    b.HasOne("DatBanNhaHang.Entities.NguoiDung.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserID");

                    b.Navigation("Admin");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NguoiDung.XacNhanEmail", b =>
                {
                    b.HasOne("DatBanNhaHang.Entities.NguoiDung.Admin", "Admin")
                        .WithMany("emails")
                        .HasForeignKey("AdminID");

                    b.HasOne("DatBanNhaHang.Entities.NguoiDung.User", "user")
                        .WithMany("xacNhanEmails")
                        .HasForeignKey("UserID");

                    b.Navigation("Admin");

                    b.Navigation("user");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NhaHang.Ban", b =>
                {
                    b.HasOne("DatBanNhaHang.Entities.NhaHang.LoaiBan", "LoaiBan")
                        .WithMany("ban")
                        .HasForeignKey("LoaiBanID");

                    b.Navigation("LoaiBan");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NhaHang.ChiTietHoaDon", b =>
                {
                    b.HasOne("DatBanNhaHang.Entities.NhaHang.HoaDon", "HoaDon")
                        .WithMany("chiTietHoaDon")
                        .HasForeignKey("HoaDonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatBanNhaHang.Entities.NhaHang.MonAn", "monAn")
                        .WithMany()
                        .HasForeignKey("MonAnID");

                    b.Navigation("HoaDon");

                    b.Navigation("monAn");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NhaHang.HoaDon", b =>
                {
                    b.HasOne("DatBanNhaHang.Entities.NhaHang.Ban", "Ban")
                        .WithMany()
                        .HasForeignKey("BanID");

                    b.HasOne("DatBanNhaHang.Entities.NhaHang.KhachHang", "KhachHang")
                        .WithMany("hoaDon")
                        .HasForeignKey("KhachHangID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatBanNhaHang.Entities.NhaHang.TrangThaiHoaDon", "TrangThaiHoaDon")
                        .WithMany("hoaDon")
                        .HasForeignKey("TrangThaiHoaDonID");

                    b.Navigation("Ban");

                    b.Navigation("KhachHang");

                    b.Navigation("TrangThaiHoaDon");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NhaHang.KhachHang", b =>
                {
                    b.HasOne("DatBanNhaHang.Entities.NguoiDung.User", "User")
                        .WithMany("khachHangs")
                        .HasForeignKey("userID");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NhaHang.MonAn", b =>
                {
                    b.HasOne("DatBanNhaHang.Entities.NhaHang.LoaiMonAn", "LoaiMonAn")
                        .WithMany("MonAn")
                        .HasForeignKey("LoaiMonAnID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoaiMonAn");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NguoiDung.Admin", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("RefreshTokens");

                    b.Navigation("emails");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NguoiDung.Role", b =>
                {
                    b.Navigation("Admin");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NguoiDung.User", b =>
                {
                    b.Navigation("RefreshTokens");

                    b.Navigation("khachHangs");

                    b.Navigation("xacNhanEmails");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NhaHang.HoaDon", b =>
                {
                    b.Navigation("chiTietHoaDon");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NhaHang.KhachHang", b =>
                {
                    b.Navigation("hoaDon");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NhaHang.LoaiBan", b =>
                {
                    b.Navigation("ban");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NhaHang.LoaiMonAn", b =>
                {
                    b.Navigation("MonAn");
                });

            modelBuilder.Entity("DatBanNhaHang.Entities.NhaHang.TrangThaiHoaDon", b =>
                {
                    b.Navigation("hoaDon");
                });
#pragma warning restore 612, 618
        }
    }
}
