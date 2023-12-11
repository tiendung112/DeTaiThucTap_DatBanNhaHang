﻿using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;

namespace DatBanNhaHang.Payloads.Converters.NhaHang
{
    public class BanConverters
    {
        public BanDTOs EntityToDTOs(Ban ban)
        {

            return new BanDTOs() {
                BanID = ban.id,
                GiaTien = ban.GiaTien,
                LoaiBanID = ban.LoaiBanID,
                SoBan = ban.SoBan,
                SoNguoiToiDa = ban.SoNguoiToiDa,
                ViTri = ban.ViTri,
                HinhAnhBanURL = ban.HinhAnhBanURL,
                Mota = ban.Mota,
                TinhTrangHienTai = ban.TinhTrangHienTai
            };
        }
    }
}
