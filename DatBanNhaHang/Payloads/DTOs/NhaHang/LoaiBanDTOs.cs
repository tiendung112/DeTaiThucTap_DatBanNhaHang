﻿namespace DatBanNhaHang.Payloads.DTOs.NhaHang
{
    public class LoaiBanDTOs
    {

        public int LoaiBanID { get; set; }
        public string? TenLoaiBan { get; set; }
        public IEnumerable<BanDTOs>? bans { get; set; }
    }
}
