using DatBanNhaHang.Context;
using DatBanNhaHang.Handler.Email;
using System;
using System.Globalization;
using System.Net.Mail;
using System.Net;
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
        public int GenerateCodeActive()
        {
            Random random = new Random();
            return random.Next(100000, 999999);
        }
        public string SendEmail(EmailTo emailTo)
        {
            if (!Validate.IsValidEmail(emailTo.Mail))
            {
                return "Định dạng email không hợp lệ";
            }
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("dung0112.dev.test@gmail.com", "xssyibnbzpqhmzsz"),
                EnableSsl = true
            };
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress("dung0112.dev.test@gmail.com");
                message.To.Add(emailTo.Mail);
                message.Subject = emailTo.Subject;
                message.Body = emailTo.Content;
                message.IsBodyHtml = true;
                smtpClient.Send(message);

                return "Gửi email thành công";
            }
            catch (Exception ex)
            {
                return "Lỗi khi gửi email: " + ex.Message;
            }
        }
        public string ChuanHoaChuoi(string? chuoi)
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