namespace DatBanNhaHang.Entities.NhaHang
{
    public class TimeOrder :BaseEntity
    {
        public int HoaDonID { get; set; }
        public HoaDon? HoaDon { get; set; }
        public int BanID { get; set; }
        public Ban? Ban { get; set; }
    
        public DateTime? timeOrderIn {  get; set; }
        public DateTime? timeOrderOut { get; set; }
    }
}
