namespace DatBanNhaHang.Entities.NguoiDung
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public DateTime ExpiredTime { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
