﻿namespace DatBanNhaHang.Payloads.Requests.NhaHang.KhachHang
{
    public class Request_SuaKhachHang
    {
        public int id {  get; set; }
        public string? HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? DiaChi { get; set; }
        public string? SDT { get; set; }
    }
}
