namespace DatBanNhaHang.Entities.NguoiDung
{
    public class Admin :BaseEntity
    {
        public string AdminName { get; set; }
        public string password { get; set; }
        public string Name { get; set; }
        public int RoleID { get; set; }
        public Role? Role { get; set; }
    }
}
