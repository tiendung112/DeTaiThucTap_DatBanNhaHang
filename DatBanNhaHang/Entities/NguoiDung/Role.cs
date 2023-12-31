﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DatBanNhaHang.Entities.NguoiDung
{
    [Table("Role")]
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }
        public IEnumerable<Admin> Admin { get; set; }
    }
}