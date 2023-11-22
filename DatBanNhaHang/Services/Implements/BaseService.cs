using DatBanNhaHang.Context;
using System;
using System.Globalization;
using System.Text;

namespace DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements
{
    public class BaseService
    {
        public readonly AppDbContext contextDB;
        public BaseService()
        {
            contextDB = new AppDbContext();
        }

        public string ChuanHoaChuoi(string chuoi)
        {
            string boDauCach = chuoi.Trim().ToLower();//xoá dấu cách đầu cuối và viết thường all
            string xoaDau = new string(boDauCach.Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray()); // xoá dấu 

            while (xoaDau.Contains("  "))
            {
                xoaDau = xoaDau.Replace("  ", " ");//thay the nhieu phim cach thanh 1 phim cach
            }
            //xoá các dấu cách còn dư 
            string chuoiCh = xoaDau;
            return chuoiCh;
        }
    }
}