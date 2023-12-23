using DatBanNhaHang.Entities.NguoiDung;
using DatBanNhaHang.Entities.NhaHang;
using Microsoft.EntityFrameworkCore;

namespace DatBanNhaHang.Context
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<LoaiMonAn> LoaiMonAn { get; set; }
        public virtual DbSet<MonAn> MonAn { get; set; }
        public virtual DbSet<Ban> Ban { get; set; }
        public virtual DbSet<LoaiBan> LoaiBan { get; set; }
        public virtual DbSet<HoaDon> HoaDon { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDon { get; set; }
        public virtual DbSet<TrangThaiHoaDon> TrangThaiHoaDon { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        public virtual DbSet<DauBep> DauBep { get; set; }
        public virtual DbSet<RefreshToken> RefreshToken { get; set; }
        public virtual DbSet<XacNhanEmail> XacNhanEmail { get; set; }
        public virtual DbSet<LienHe> LienHe { get; set; }
        public virtual DbSet<BaiViet> BaiViet { get; set; }
        public virtual DbSet<NhanXet> NhanXet { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(SourseData.MyConnect());
        }
    }
}
