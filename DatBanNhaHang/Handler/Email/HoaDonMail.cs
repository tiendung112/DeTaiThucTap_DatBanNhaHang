using DatBanNhaHang.Context;
using DatBanNhaHang.Entities.NhaHang;

namespace DatBanNhaHang.Handler.Email
{
    public class HoaDonMail
    {
        private static readonly AppDbContext context = new AppDbContext();
        public static string GenerateNotificationBillEmail(HoaDon hoadon, string message = "")
        {
            var khachhang = context.User.SingleOrDefault(x => x.id == hoadon.userId && x.status == 1);
            string htmlContent = $@"
            <html>
            <head>
                <style> 
                    body {{
                        font-family: Arial, sans-serif;
                    }}
                    image {{
                        width: 60px;
                        height: 70px;
                    }}
                    h1 {{
                        color: #333;
                    }}
                    
                    table {{
                        border-collapse: collapse;
                        width: 100%;
                    }}
                    
                    th, td {{
                        border: 1px solid #ddd;
                        padding: 8px;
                    }}
                    
                    th {{
                        background-color: #f2f2f2;
                        font-weight: bold;
                    }}
                    
                    .footer {{
                        margin-top: 20px;
                        font-size: 14px;
                    }}
                </style>
            </head>
            <body>
                <h1>Thông tin hóa đơn đã đặt {hoadon.MaGiaoDich}</h1>
                <h2 style=""color: red; font-size: 20px; font-weight: bold;"">{(string.IsNullOrEmpty(message) ? "" : message)}</h2>
                <h2> Bàn : {hoadon.BanID} </h2>
                <h2> Thời gian đặt  : {hoadon.ThoiGianDat} </h2>
                <h2> Thời gian dự kiến bắt đầu : {hoadon.ThoiGianDuKienBatDau} </h2>
                <h2> Tên khách hàng {khachhang.Name}  </h2>
                <h2> Ngày sinh {khachhang.DateOfBirth} </h2>
                
                ";
            htmlContent += @"<h2>Chi tiết món ăn đã đặt</h2>
                <table>
                    <tr>
                        <th style=""text-align: center;"">{STT}</th>
                        <th>Tên món ăn</th>
                        <th>Số lượng</th>
                        
                        <th>Giá</th>
                    </tr>";
            //int rowIndex = 1;
            //var orderdetail = context.ChiTietHoaDon.Where(x => x.HoaDonID == hoadon.id).ToList();
            //foreach (var item in orderdetail)
            //{
            //    var sp = context.MonAn.SingleOrDefault(x => x.id == item.MonAnID);
            //    htmlContent += $@"
            //        <tr>
            //            //<td>{rowIndex}</td>
            //            //<td>{sp.TenMon}</td>
            //            <td>{item.SoLuong}</td>
                        
            //            <td>{item.ThanhTien}</td>
            //        </tr>";
            //    //rowIndex++;
            //}

            htmlContent += $@"
                       <tr>
                        <td style=""text-align: center;"">Tổng tiền</td>
                        <td colspan=""3"" style=""text-align: right;"">{hoadon.TongTien}</td>
                    </tr>
                </table>
                
                <h2> Ghi chú {hoadon.GhiChu} </h2>
                <div class=""footer"">
                    <p>Trân trọng bạn đã tin tưởng nhà hàng chúng tôi</p>
                    <p></p>
                </div>
            </body>
            </html>";

            return htmlContent;
        }
    }
}
