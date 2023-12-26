using System.Reflection.Metadata;
using DatBanNhaHang.Entities.NguoiDung;
using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Handler.Email;
using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.Converters.NhaHang;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NguoiDung.User;
using DatBanNhaHang.Payloads.Requests.NhaHang.ChiTietHoaDon;
using DatBanNhaHang.Payloads.Requests.NhaHang.HoaDon;
using DatBanNhaHang.Payloads.Responses;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;
using DatBanNhaHang.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace DatBanNhaHang.Services.Implements
{
    public class HoaDonServices : BaseService, IHoaDon
    {
        private readonly ResponseObject<HoaDonDTO> response;
        private readonly HoaDonConverters converters;
        private readonly BanConverters banConverters;
        private readonly ResponseObject<List<BanDTOs>> responseBan;
        public HoaDonServices()
        {
            response = new ResponseObject<HoaDonDTO>();
            converters = new HoaDonConverters();
            banConverters = new BanConverters();
            responseBan = new ResponseObject<List<BanDTOs>>();
        }
        public Task<ResponseObject<HoaDonDTO>> SuaHoaDon()
        {
            throw new NotImplementedException();
        }

        #region tìm bàn trống , hiển thị bàn trống

        public async Task<ResponseObject<List<BanDTOs>>> TimBanTrong(Request_timBanTrong request)
        {
            var tatCaBan = await contextDB.Ban.ToListAsync();
            if (request.thoiGianKetThuc < request.thoiGianBatDau)
            {
                return responseBan.ResponseError(StatusCodes.Status400BadRequest, "Lỗi nhập ngày kết thúc bé hơn ngày bắt đầu",null);
            }
            if(request.thoiGianBatDau <= DateTime.Now)
            {
                return responseBan.ResponseError(StatusCodes.Status400BadRequest, "Thời gian bắt đầu phải lớn hơn thời gian hiện tại", null);
            }
            if(request.thoiGianKetThuc <= request.thoiGianBatDau)
            {
                return responseBan.ResponseError(StatusCodes.Status400BadRequest, "Thời gian kết thúc phải lớn hơn thời gian bắt đầu", null);
            }
            var banTrong = new List<BanDTOs>();
           
            foreach (var ban in tatCaBan)
            {
                if (await KiemTraBanTrong(ban.id, request.thoiGianBatDau, request.thoiGianKetThuc))
                {
                    banTrong.Add(banConverters.EntityToDTOs(ban));
                }
            }
            return responseBan.ResponseSuccess($"các bàn trống ở {request.thoiGianBatDau.ToString("dd/MM/yyyy HH:mm")} đến {request.thoiGianKetThuc.ToString("dd/MM/yyyy HH:mm")} ", banTrong);
        }
        public async Task<ResponseObject<List<BanDTOs>>> HienThiBanTrong()
        {
            var tatCaBan = await contextDB.Ban.ToListAsync();
            var banTrong = new List<BanDTOs>();

            foreach (var ban in tatCaBan)
            {
                if (await KiemTraBanTrong(ban.id, DateTime.Now, DateTime.Now.AddHours(1)))
                {
                    banTrong.Add(banConverters.EntityToDTOs(ban));
                }
            }
            return responseBan.ResponseSuccess("các bàn trống ở thời điểm hiện tại", banTrong);
        }

        public async Task<bool> KiemTraBanTrong(int? banId, DateTime thoiGianBatDauDuKien, DateTime thoiGianKetThucDuKien)
        {
            var hoaDons = await contextDB.HoaDon
                                .Where(hd => hd.BanID == banId
                                             && hd.TrangThaiHoaDonID == 2 ||hd.TrangThaiHoaDonID==3 && hd.status==1)
                                .ToListAsync();
            foreach (var hoaDon in hoaDons)
            {
                // Sử dụng thời gian thực tế nếu có, ngược lại sử dụng thời gian dự kiến
                var thoiGianBatDau = hoaDon.ThoiGianBatDauThucTe ?? hoaDon.ThoiGianDuKienBatDau;
                var thoiGianKetThuc = hoaDon.ThoiGianKetThucThucTe ?? hoaDon.ThoiGianDuKienKetThuc;

                // Kiểm tra xung đột thời gian
                if ((thoiGianBatDau < thoiGianKetThucDuKien) && (thoiGianBatDauDuKien < thoiGianKetThuc))
                {
                    return false; // Có xung đột thời gian, bàn không trống
                }
            }
            return true; // Không có xung đột, bàn trống
        }

        #endregion
        #region hoá đơn by user 
        private string TaoMaGiaoDich(DateTime nt, List<HoaDon> ct)
        {
            //newHoaDon.MaGiaoDich = $"{newHoaDon.ThoiGianTao.Year}" +
            //                $"{newHoaDon.ThoiGianTao.Month}" +
            //                $"{newHoaDon.ThoiGianTao.Day}" +
            //                $"_{String.Format("{0:000}", lstHoaDon.Count() + 1)}";
            string maGiaoDich =nt.ToString("dd/MM/yyyy") + $"T{ct.Count() + 1}";
            return maGiaoDich;
        }

        public async Task<ResponseObject<HoaDonDTO>> ThemHoaDonUser(int userid, Request_ThemHoaDon_User request)
        {
            if (!contextDB.Ban.Any(x => x.id == request.BanID && x.status==1))
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại bàn này", null);
            }
            else
            {
                if (await KiemTraBanTrong(request.BanID, request.ThoiGianDuKienBatDau,
                        request.ThoiGianDuKienBatDau.AddHours(1)) == true)
                {
                    var kh = contextDB.User.SingleOrDefault(x => x.id == userid && x.status==1 );

                    HoaDon newHoaDon = new HoaDon()
                    {
                        BanID = request.BanID,
                        ThoiGianDat = DateTime.Now,
                        TenHoaDon = $"Đặt Bàn {contextDB.Ban.SingleOrDefault(x => x.id == request.BanID && x.status==1)
                            .SoBan}" +
                                    $" Thời Gian : {DateTime.Now.ToString("dd/MM/yyyy)")}",
                        MaGiaoDich = TaoMaGiaoDich(request.ThoiGianDuKienBatDau,
                            contextDB.HoaDon.Where(x => x.ThoiGianDat.Value.Date == DateTime.Now.Date && x.status==1)
                                .ToList()),
                        userId = kh.id,
                        ThoiGianDuKienBatDau = request.ThoiGianDuKienBatDau,
                        ThoiGianBatDauThucTe = request.ThoiGianDuKienBatDau,
                        ThoiGianDuKienKetThuc = request.ThoiGianDuKienBatDau.AddHours(1),
                        GhiChu = request.GhiChu,
                        TrangThaiHoaDonID = 1,
                        TongTien = contextDB.Ban.SingleOrDefault(x => x.id == request.BanID && x.status==1).GiaTien,
                        status = 1,
                    };
                    await contextDB.AddAsync(newHoaDon);
                    await contextDB.SaveChangesAsync();
                    return response.ResponseSuccess("Bạn đã đặt bàn thành công", converters.EntityToDTOs(newHoaDon));
                }
                else
                    return response.ResponseError(StatusCodes.Status404NotFound, "bàn không trống", null);
            }
            /* List<ChiTietHoaDon> chitiet = await ThemChiTietHoaDon(newHoaDon.id, request.ChiTietHoaDonDTOs.ToList());
             newHoaDon.chiTietHoaDon = chitiet;
             newHoaDon.TongTien = chitiet.Sum(x => x.ThanhTien);
             contextDB.Update(newHoaDon);
             await contextDB.SaveChangesAsync();*/

                //xoá thông tin xác nhận email
                /*var confrim = contextDB.XacNhanEmail.Where(x => x.UserID == userid);
                contextDB.XacNhanEmail.RemoveRange(confrim);
                await contextDB.SaveChangesAsync();

                XacNhanEmail confrimEmail = new XacNhanEmail()
                {
                    UserID = userid,
                    DaXacNhan = false,
                    ThoiGianHetHan = DateTime.Now.AddMinutes(30),
                    MaXacNhan = GenerateCodeActive().ToString(),
                };
                await contextDB.XacNhanEmail.AddAsync(confrimEmail);
                await contextDB.SaveChangesAsync();
                string mss = SendEmail(new EmailTo
                {
                    Mail = contextDB.User.SingleOrDefault(x => x.id == kh.userID).Email,
                    Subject = "Nhận mã xác nhận để xác nhận đặt bàn: ",
                    Content = $"Mã kích hoạt của bạn là: {confrimEmail.MaXacNhan}, mã này sẽ hết hạn sau 30 phút"
                });
                return response.ResponseSuccess
                    ("Bạn đã gửi yêu cầu đặt bàn ," +
                    $" vui lòng nhập mã xác nhận đã được gửi về email {contextDB.User.SingleOrDefault(x => x.id == kh.userID).Email}"
                    , converters.EntityToDTOs(newHoaDon));*/
                
        }
      /*  private async Task<List<ChiTietHoaDon>> ThemChiTietHoaDon(int hoadonId, List<Request_ThemChiTietHoaDon> cthd)
        {
            List<ChiTietHoaDon> chiTietHoaDon = new List<ChiTietHoaDon>();
            foreach (var item in cthd)
            {
                var monan = contextDB.MonAn.SingleOrDefault(x => x.id == item.MonAnID);
                if (monan == null)
                {
                    return null;
                }
                ChiTietHoaDon newCT = new ChiTietHoaDon()
                {
                    MonAnID = monan.id,
                    SoLuong = item.SoLuong,
                    ThanhTien = monan.GiaTien * item.SoLuong,
                };
                chiTietHoaDon.Add(newCT);
            }
            return chiTietHoaDon;
        }*/
       /* public async Task<string> XacNhanOrder(int id, int hoadonid, Request_ValidateRegister request)
        {
            XacNhanEmail confirmEmail = await contextDB.XacNhanEmail
                .Where(x => x.MaXacNhan.Equals(request.MaXacNhan))
                .FirstOrDefaultAsync();
            if (confirmEmail is null)
            {
                return "Mã xác nhận không chính xác";
            }
            if (confirmEmail.ThoiGianHetHan < DateTime.Now)
            {
                return "Mã xác nhận đã hết hạn";
            }
            if (confirmEmail.UserID == id)
            {
                var kh = contextDB.KhachHang.SingleOrDefault(x => x.userID == confirmEmail.UserID);
                HoaDon hoadon = contextDB.HoaDon.SingleOrDefault(x => x.id == hoadonid);
                hoadon.TrangThaiHoaDonID = 2;
                contextDB.XacNhanEmail.Remove(confirmEmail);
                contextDB.HoaDon.Update(hoadon);
                await contextDB.SaveChangesAsync();
                string mss = SendEmail(new EmailTo
                {
                    Mail = contextDB.User.SingleOrDefault(x => x.id == kh.userID).Email,
                    Subject = "Đã đặt bàn thành công tại nhà hàng ",
                    Content = Handler.Email.HoaDonMail.GenerateNotificationBillEmail(hoadon, $"Bạn đã đặt bàn thành công bàn số {hoadon.BanID}")
                });
                return "Xác nhận đặt bàn thành công thành công đã gửi mail cho quý khách";
            }
            return "huỷ bàn thất bại";

        }*/

        public async Task<ResponseObject<HoaDonDTO>> SuaHoaDon(int userid, int hoadonid, Request_SuaHoaDon_User request)
        {
            var hoaDon = contextDB.HoaDon.SingleOrDefault(x => x.id == hoadonid && x.status==1);
            if (hoaDon == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "không tìm thấy đơn đặt hàng này", null);
            }

            var kh = contextDB.User.SingleOrDefault(x => x.id == userid && x.status==1);
            if (kh.id == hoaDon.userId)
            {
                if(!contextDB.Ban.Any(x=>x.id== request.BanID && x.status==1))
                {
                    return response.ResponseError(StatusCodes.Status404NotFound, "không tồn tại bàn này", null);
                }
                else
                {
                    if (await KiemTraBanTrong(request.BanID, request.ThoiGianDuKienBatDau,
                            request.ThoiGianDuKienBatDau.AddHours(1)) == true)
                    {
                        hoaDon.BanID = request.BanID;
                        hoaDon.GhiChu =request.GhiChu?? hoaDon.GhiChu ;
                    }
                    contextDB.Update(hoaDon);
                    await contextDB.SaveChangesAsync();
                    return response.ResponseSuccess("Sửa đơn đặt bàn thành công", converters.EntityToDTOs(hoaDon));

                }
            }
            return response.ResponseError(StatusCodes.Status404NotFound, "không tìm thấy đơn đặt hàng này", null);
            /*if (request.ThoiGianDuKienBatDau != null && await KiemTraBanTrong(hoaDon.BanID, request.ThoiGianDuKienBatDau, request.ThoiGianDuKienBatDau.AddHours(1)) == true)
            {
                hoaDon.ThoiGianDuKienBatDau = request.ThoiGianDuKienBatDau;
                hoaDon.ThoiGianDuKienKetThuc = request.ThoiGianDuKienBatDau.AddHours(1);
                hoaDon.GhiChu = request.GhiChu;
            }
            if (hoaDon.TrangThaiHoaDonID == 2)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "đơn đặt hàng đã được xác nhận không thể thay đổi ", converters.EntityToDTOs(hoaDon));
            }
            var lstCTHd = contextDB.ChiTietHoaDon.Where(x => x.HoaDonID == hoadonid);
            if (status == 0) //status =0 , thêm món
            {
                foreach (var item in request.chitiet.ToList())
                {
                    var ma = contextDB.MonAn.SingleOrDefault(x => x.id == item.MonAnID);
                    ChiTietHoaDon ct = new ChiTietHoaDon()
                    {
                        HoaDonID = hoadonid,
                        MonAnID = item.MonAnID,
                        SoLuong = item.SoLuong,

                        ThanhTien = ma.GiaTien * item.SoLuong,
                    };
                    contextDB.ChiTietHoaDon.Add(ct);
                    await contextDB.SaveChangesAsync();
                }
            }
            else if (status == 1) //bớt món
            {
                // Xóa chi tiết hóa đơn không xuất hiện trong request
                var chiTietRequestIds = request.chitiet.Select(ct => ct.MonAnID).ToList();
                var chiTietToRemove = lstCTHd.Where(ct => !chiTietRequestIds.Contains(ct.MonAnID));
                contextDB.ChiTietHoaDon.RemoveRange(chiTietToRemove);

                // Cập nhật chi tiết hóa đơn còn lại
                foreach (var item in request.chitiet)
                {
                    var chiTietExisting = lstCTHd.FirstOrDefault(ct => ct.MonAnID == item.MonAnID);
                    if (chiTietExisting != null)
                    {
                        var ma = contextDB.MonAn.SingleOrDefault(x => x.id == item.MonAnID);
                        chiTietExisting.SoLuong = item.SoLuong;
                        chiTietExisting.ThanhTien = ma.GiaTien * item.SoLuong;
                        contextDB.ChiTietHoaDon.Update(chiTietExisting);
                    }
                }
                await contextDB.SaveChangesAsync();
            }
            else //thêm lại món
            {
                contextDB.ChiTietHoaDon.RemoveRange(lstCTHd);
                await contextDB.SaveChangesAsync();
                foreach (var item in request.chitiet.ToList())
                {
                    var ma = contextDB.MonAn.SingleOrDefault(x => x.id == item.MonAnID);
                    ChiTietHoaDon ct = new ChiTietHoaDon()
                    {
                        HoaDonID = hoadonid,
                        MonAnID = item.MonAnID,
                        SoLuong = item.SoLuong,

                        ThanhTien = ma.GiaTien * item.SoLuong,
                    };
                    contextDB.ChiTietHoaDon.Add(ct);
                    await contextDB.SaveChangesAsync();
                }
            }

            var cthd = contextDB.ChiTietHoaDon.Where(x => x.HoaDonID == hoadonid);
            hoaDon.TongTien = cthd.Sum(x => x.ThanhTien);
            contextDB.HoaDon.Update(hoaDon);
            await contextDB.SaveChangesAsync();*/
            //xoá thông tin xác nhận email
            /*var confrim = contextDB.XacNhanEmail.Where(x => x.UserID == userid);
            contextDB.XacNhanEmail.RemoveRange(confrim);
            await contextDB.SaveChangesAsync();

            XacNhanEmail confrimEmail = new XacNhanEmail()
            {
                UserID = userid,
                DaXacNhan = false,
                ThoiGianHetHan = DateTime.Now.AddMinutes(30),
                MaXacNhan = GenerateCodeActive().ToString(),
            };
            await contextDB.XacNhanEmail.AddAsync(confrimEmail);
            await contextDB.SaveChangesAsync();
            string mss = SendEmail(new EmailTo
            {
                Mail = contextDB.User.SingleOrDefault(x => x.id == khachHang.userID).Email,
                Subject = "Nhận mã xác nhận để xác nhận đặt bàn: ",
                Content = $"Mã kích hoạt của bạn là: {confrimEmail.MaXacNhan}, mã này sẽ hết hạn sau 30 phút"
            });
            return response.ResponseSuccess
                ("Bạn đã gửi yêu cầu đặt bàn ," +
                $" vui lòng nhập mã xác nhận đã được gửi về email {contextDB.User.SingleOrDefault(x => x.id == khachHang.userID).Email}"
                , converters.EntityToDTOs(hoaDon));*/

        }
        public async Task<ResponseObject<HoaDonDTO>> HuyHoaDon(int hoadonid, int userid)
        {
            var hoaDon = contextDB.HoaDon.SingleOrDefault(x => x.id == hoadonid);
            if (hoaDon == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "không tìm thấy đơn đặt hàng này", null);
            }
            var khachHang = contextDB.User.SingleOrDefault(x => x.id == userid && x.status==1);
            if (khachHang==null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "không tồn tại khách hàng này", null);
            }
            if (khachHang.id == hoaDon.userId)
            {
                if (hoaDon.TrangThaiHoaDonID == 3)
                {
                    return response.ResponseError(StatusCodes.Status410Gone, "đơn đặt bàn này đã hoàn thành ", null);
                }
                else if (hoaDon.TrangThaiHoaDonID==1)
                {
                    hoaDon.status = 2;
                    return response.ResponseSuccess("Huỷ đơn đặt bàn thành công",converters.EntityToDTOs(hoaDon));
                }
                
                //xoá thông tin xác nhận email
                var confrim = contextDB.XacNhanEmail.Where(x => x.UserID == userid);
                contextDB.XacNhanEmail.RemoveRange(confrim);
                await contextDB.SaveChangesAsync();
                
                XacNhanEmail confrimEmail = new XacNhanEmail()
                {
                    UserID = userid,
                    DaXacNhan = false,
                    ThoiGianHetHan = DateTime.Now.AddMinutes(30),
                    MaXacNhan = GenerateCodeActive().ToString(),
                };
                await contextDB.XacNhanEmail.AddAsync(confrimEmail);
                await contextDB.SaveChangesAsync();
                string mss = SendEmail(new EmailTo
                {
                    Mail = contextDB.User.SingleOrDefault(x => x.id == khachHang.id).Email,
                    Subject = "Nhận mã xác nhận để xác nhận huỷ đặt bàn: ",
                    Content = $"Mã kích hoạt của bạn là: {confrimEmail.MaXacNhan}, mã này sẽ hết hạn sau 30 phút"
                });
                return response.ResponseSuccess
                    ("Bạn đã gửi yêu cầu huỷ đặt bàn ," +
                    $" vui lòng nhập mã xác nhận đã được gửi về email {contextDB.User.SingleOrDefault(x => x.id == khachHang.id).Email}"
                    , converters.EntityToDTOs(hoaDon));
            }
            return response.ResponseError(StatusCodes.Status404NotFound, "không tìm thấy đơn đặt hàng này", null);
        }

        public async Task<string> XacNhanHuyOrder(int id, int hoadonid, Request_ValidateRegister request)
        {
            XacNhanEmail confirmEmail = await contextDB.XacNhanEmail
                .Where(x => x.MaXacNhan.Equals(request.MaXacNhan))
                .FirstOrDefaultAsync();
            if (confirmEmail is null)
            {
                return "Mã xác nhận không chính xác";
            }
            if (confirmEmail.ThoiGianHetHan < DateTime.Now)
            {
                return "Mã xác nhận đã hết hạn";
            }
            if (confirmEmail.UserID == id)
            {
                var kh = contextDB.User.SingleOrDefault(x => x.id == confirmEmail.UserID);
                HoaDon hoadon = contextDB.HoaDon.SingleOrDefault(x => x.id == hoadonid);
                hoadon.ThoiGianHuyDat = DateTime.Now;
                hoadon.ThoiGianDuKienBatDau = null;
                hoadon.ThoiGianDuKienKetThuc = null;
                hoadon.status = 2;
                contextDB.XacNhanEmail.Remove(confirmEmail);
                contextDB.HoaDon.Update(hoadon);
                await contextDB.SaveChangesAsync();
                string mss = SendEmail(new EmailTo
                {
                    Mail = contextDB.User.SingleOrDefault(x => x.id == kh.id).Email,
                    Subject = $"Đã huỷ đơn hàng {hoadon.id}",
                    Content =Handler.Email.HoaDonMail.GenerateNotificationBillEmail(hoadon,$"Bạn đã huỷ bàn {hoadon.BanID}")
                });
                return "Xác nhận huỷ bàn thành công" + hoadon.id;
            }
            return "Xác nhận huỷ bàn thất bại";
        }
        #endregion
        #region hoá đơn by admin
        //lưu thông tin khách hàng đến và đi giờ nào cũng như thanh toán 
        public async Task<ResponseObject<HoaDonDTO>> CapNhatThongTinHoaDon(int id )
        {
            var hoadon = contextDB.HoaDon.SingleOrDefault(x => x.id == id && x.status==1);
            if (hoadon == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại hoá đơn này", null);
            }
            if (hoadon.TrangThaiHoaDonID == 1)
            {
                hoadon.TrangThaiHoaDonID = 2;
                contextDB.Update(hoadon);
                await contextDB.SaveChangesAsync();
                string mss = SendEmail(new EmailTo
                {
                    Mail = contextDB.User.SingleOrDefault(x => x.id == hoadon.userId && x.status==1).Email,
                    Subject = "Đặt bàn thành công tại nhà hàng ",
                    
                    Content = Handler.Email.HoaDonMail.GenerateNotificationBillEmail(hoadon, $"Bạn đã đặt bàn thành công bàn số {hoadon.BanID}")
                });
                return response.ResponseSuccess("Xác nhận đơn đặt bàn thành công",
                    converters.EntityToDTOs(hoadon));
            }
            else 
            {
                hoadon.ThoiGianKetThucThucTe = DateTime.Now;
                hoadon.TrangThaiHoaDonID = 3;
                contextDB.Update(hoadon);
                await contextDB.SaveChangesAsync();
                return response.ResponseSuccess("Hoàn thành đơn đặt bàn",
                    converters.EntityToDTOs(hoadon));
            }
        }
        /*public async Task<ResponseObject<HoaDonDTO>> ThemHoaDonAdmin(Request_ThemHoaDon_Admin request)
        {
            if (await KiemTraBanTrong(request.BanID, DateTime.Now, DateTime.Now.AddHours(1)) == true)
            {
                var kh = contextDB.User.SingleOrDefault(x => x.id == request.Khid);

                if (!contextDB.Ban.Any(x => x.id == request.BanID))
                {
                    return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại bàn này", null);
                }
                if (kh == null)
                {
                    return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại khách hàng này", null);
                }
                HoaDon newHoaDon = new HoaDon()
                {
                    BanID = request.BanID,
                    ThoiGianDat = DateTime.Now,
                    TenHoaDon = $"Đặt Bàn {contextDB.Ban.SingleOrDefault(x => x.id == request.BanID).SoBan} ngay{DateTime.Now}",
                    MaGiaoDich = TaoMaGiaoDich(DateTime.Now, contextDB.HoaDon.Where(x => x.ThoiGianDat.Value.Date == DateTime.Now.Date).ToList()),
                    userId = kh.id,
                    ThoiGianDuKienBatDau = DateTime.Now,
                    ThoiGianBatDauThucTe = DateTime.Now,
                    ThoiGianDuKienKetThuc = DateTime.Now.AddHours(1),
                    GhiChu = request.GhiChu,
                    TrangThaiHoaDonID = 2,
                };
                await contextDB.AddAsync(newHoaDon);
                await contextDB.SaveChangesAsync();

                //List<ChiTietHoaDon> chitiet = await ThemChiTietHoaDon(newHoaDon.id, request.ChiTietHoaDonDTOs.ToList());
                //newHoaDon.chiTietHoaDon = chitiet;
                //newHoaDon.TongTien = chitiet.Sum(x => x.ThanhTien);
                //contextDB.Update(newHoaDon);
                //await contextDB.SaveChangesAsync();

                return response.ResponseSuccess("Đặt bàn thành công", converters.EntityToDTOs(newHoaDon));
            }
            return response.ResponseError(StatusCodes.Status404NotFound, "bàn không trống", null);

        }*/
        public async Task<string> XoaTatCaHoaDonChuaDuyet()
        {
            var thoiGianHienTai = DateTime.Now;
            var hoaDonsQuaHan = contextDB.HoaDon
                .Where(hd => hd.TrangThaiHoaDonID == 1 && hd.ThoiGianDuKienBatDau.Value.AddHours(5) < thoiGianHienTai && hd.status==1)
                .ToList();

            if (hoaDonsQuaHan.Any())
            {
                foreach (var hd in (hoaDonsQuaHan))
                {
                    hd.status = 2;
                    //contextDB.HoaDon.RemoveRange(hoaDonsQuaHan);
                }
            }
            contextDB.Update(hoaDonsQuaHan);
            await contextDB.SaveChangesAsync();
            return "đã xoá tất cả hoá đơn chưa duyệt ";
        }

        public async Task<ResponseObject<HoaDonDTO>> XoaHoaDonAdmin(int HoaDonid)
        {
            var hoaDon = contextDB.HoaDon.SingleOrDefault(x => x.id == HoaDonid && x.status==1);
            if (hoaDon == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "không tìm thấy đơn đặt hàng này", null);
            }
            hoaDon.status = 2;
            contextDB.Update(hoaDon);
            await contextDB.SaveChangesAsync();

            return response.ResponseSuccess("Xoá hoá đơn thành công", converters.EntityToDTOs(hoaDon));
        }
        public async Task<ResponseObject<HoaDonDTO>> SuaHoaDonAdmin(int hoaDonid, Request_SuaHoaDon request)
        {
            var hoaDon = contextDB.HoaDon.SingleOrDefault(x => x.id == hoaDonid  && x.status==1);
            if (hoaDon == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "không tìm thấy đơn đặt hàng này", null);
            }
            if (request.ThoiGianDuKienBatDau != null && await KiemTraBanTrong(hoaDon.BanID, request.ThoiGianDuKienBatDau.Value, request.ThoiGianDuKienBatDau.Value.AddHours(1)) == true)
            {
                hoaDon.ThoiGianDuKienBatDau = request.ThoiGianDuKienBatDau ?? hoaDon.ThoiGianDuKienBatDau;
                hoaDon.ThoiGianDuKienKetThuc =request.ThoiGianDuKienBatDau.Value.AddHours(1);
            }
            hoaDon.TrangThaiHoaDonID = 2;
            hoaDon.GhiChu = request.GhiChu ?? hoaDon.GhiChu;
            /*var lstCTHd = contextDB.ChiTietHoaDon.Where(x => x.HoaDonID == hoaDonid);
            if (status == 0) //status =0 , thêm món
            {
                foreach (var item in request.chitiet.ToList())
                {
                    var ma = contextDB.MonAn.SingleOrDefault(x => x.id == item.MonAnID);
                    ChiTietHoaDon ct = new ChiTietHoaDon()
                    {
                        HoaDonID = hoaDonid,
                        MonAnID = item.MonAnID,
                        SoLuong = item.SoLuong,

                        ThanhTien = ma.GiaTien * item.SoLuong,
                    };
                    contextDB.ChiTietHoaDon.Add(ct);
                    await contextDB.SaveChangesAsync();
                }
            }
            else if (status == 1) //bớt món
            {
                // Xóa chi tiết hóa đơn không xuất hiện trong request
                var chiTietRequestIds = request.chitiet.Select(ct => ct.MonAnID).ToList();
                var chiTietToRemove = lstCTHd.Where(ct => !chiTietRequestIds.Contains(ct.MonAnID));
                contextDB.ChiTietHoaDon.RemoveRange(chiTietToRemove);

                // Cập nhật chi tiết hóa đơn còn lại
                foreach (var item in request.chitiet)
                {
                    var chiTietExisting = lstCTHd.FirstOrDefault(ct => ct.MonAnID == item.MonAnID);
                    if (chiTietExisting != null)
                    {
                        var ma = contextDB.MonAn.SingleOrDefault(x => x.id == item.MonAnID);
                        chiTietExisting.SoLuong = item.SoLuong;
                        chiTietExisting.ThanhTien = ma.GiaTien * item.SoLuong;
                        contextDB.ChiTietHoaDon.Update(chiTietExisting);
                    }
                }
                await contextDB.SaveChangesAsync();
            }
            else //thêm lại món
            {
                contextDB.ChiTietHoaDon.RemoveRange(lstCTHd);
                await contextDB.SaveChangesAsync();
                foreach (var item in request.chitiet.ToList())
                {
                    var ma = contextDB.MonAn.SingleOrDefault(x => x.id == item.MonAnID);
                    ChiTietHoaDon ct = new ChiTietHoaDon()
                    {
                        HoaDonID = hoaDonid,
                        MonAnID = item.MonAnID,
                        SoLuong = item.SoLuong,

                        ThanhTien = ma.GiaTien * item.SoLuong,
                    };
                    contextDB.ChiTietHoaDon.Add(ct);
                    await contextDB.SaveChangesAsync();
                }
            }
            var cthd = contextDB.ChiTietHoaDon.Where(x => x.HoaDonID == hoaDonid);
            hoaDon.TongTien = cthd.Sum(x => x.ThanhTien);*/
            contextDB.Update(hoaDon);
            await contextDB.SaveChangesAsync();
            return response.ResponseSuccess("sửa đơn đặt hàng thành công", converters.EntityToDTOs(hoaDon));
        }

        public async Task<PageResult<HoaDonDTO>> HienThiHoaDon(int hoadonid, int pageSize, int pageNumber)
        {
            var lsthoadon = hoadonid == 0 ?
                contextDB.HoaDon.Where(y=>y.status==1).Select(x => converters.EntityToDTOs(x))
                : contextDB.HoaDon.Where(x => x.id == hoadonid&& x.status==1)
                    .Select(y => converters.EntityToDTOs(y));
            var result = Pagintation.GetPagedData(lsthoadon, pageSize, pageNumber);
            return result;
        }

        public async Task<PageResult<HoaDonDTO>> HienThiHoaDonCuaUser(int userid, int pageSize, int pageNumber)
        {
            var user = contextDB.User.SingleOrDefault(x => x.id == userid && x.status==1);
            if (user==null)
            {
                return null;
            }
            //var kh = contextDB.User.SingleOrDefault(x => x.id == user.id);
            var lsthoadon = contextDB.HoaDon
                .Where(x => x.userId == user.id && x.status==1)
                .OrderByDescending(z => z.ThoiGianKetThucThucTe)
                .Select(y => converters.EntityToDTOs(y));
            var result = Pagintation.GetPagedData(lsthoadon, pageSize, pageNumber);
            return result;
        }

        /*public async Task<PageResult<HoaDonDTO>> HienThiHoaDonCuaKhachHang(int khid, int pageSize, int pageNumber)
        {
            var kh = contextDB.KhachHang.SingleOrDefault(x => x.userID == khid);
            var lsthoadon = contextDB.HoaDon
                .Where(x => x.KhachHangID == kh.id)
                .OrderByDescending(z => z.ThoiGianKetThucThucTe)
                .Select(y => converters.EntityToDTOs(y));
            var result = Pagintation.GetPagedData(lsthoadon, pageSize, pageNumber);
            return result;
        }*/
        #endregion
    }
}
