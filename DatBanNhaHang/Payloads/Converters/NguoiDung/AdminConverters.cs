﻿using DatBanNhaHang.Context;
using DatBanNhaHang.Entities.NguoiDung;
using DatBanNhaHang.Payloads.DTOs.NguoiDung;

namespace DatBanNhaHang.Payloads.Converters.NguoiDung
{

    public class AdminConverters
    {
        private readonly AppDbContext context = new AppDbContext();
        public AdminDTOs EntityToDTOs(Admin admin)
        {
            return new AdminDTOs()
            {
                ADminID = admin.id,
                AdminName = admin.AdminName,
                Email = admin.Email,
                Gender = admin.Gender,
                ngaysinh = admin.ngaysinh,
                QueQuan = admin.QueQuan,
                create_at = admin.create_at,
                Name = admin.Name,
                NguoiTao = admin.ParentID == null ? "ADMIN" : context.Admin.SingleOrDefault(x => x.id == admin.ParentID).Name,
                RoleName = context.Role.SingleOrDefault(x => x.id == admin.RoleID).RoleName,
                SDT = admin.SDT
            };
        }
        public ProfileAdminDTOs EntityToProfileAdminDTOs(Admin admin)
        {
            return new ProfileAdminDTOs
            {
                ADminID = admin.id,
                AdminName = admin.AdminName,
                Email = admin.Email,
                Gender = admin.Gender,
                ngaysinh = admin.ngaysinh,
                QueQuan = admin.QueQuan,
                //create_at = admin.create_at,
                Name = admin.Name,
                //NguoiTao = admin.ParentID == null ? "ADMIN" : context.Admin.SingleOrDefault(x => x.id == admin.ParentID).Name,
                RoleName = context.Role.SingleOrDefault(x => x.id == admin.RoleID).RoleName,
                SDT = admin.SDT
            };
        }
    }
}
