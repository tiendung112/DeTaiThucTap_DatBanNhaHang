using DatBanNhaHang.Entities.NguoiDung;
using DatBanNhaHang.Handler.Email;
using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.DTOs.NguoiDung;
using DatBanNhaHang.Payloads.Requests.NguoiDung;
using DatBanNhaHang.Payloads.Responses;

namespace DatBanNhaHang.Services.IServices
{
    public interface IAuthServices
    {
        string GenerateRefreshToken();
        TokenDTO GenerateAccessToken(User user);
        ResponseObject<TokenDTO> RenewAccessToken(TokenDTO request);
        Task<ResponseObject<TokenDTO>> Login(Request_Login request);
        Task<ResponseObject<UserDTO>> RegisterRequest(Request_Register request);
        Task<PageResult<UserDTO>> GetAlls(int pageSize, int pageNumber);
        Task<ResponseObject<UserDTO>> ChangePassword(int UserID, Request_ChangePassword request);
        string SendEmail(EmailTo emailTo);
        Task<string> ForgotPassword(Request_ForgotPassword request);
        Task<ResponseObject<UserDTO>> CreateNewPassword(Request_ConfirmCreateNewPassword request);
        Task<string> XacNhanDangKyTaiKhoan(Request_ValidateRegister request);
        string RemoveTKNotActive();
        Task<string> ThayDoiQuyenHan(Request_ThayDoiQuyen request);
        //Task<ResponseObject<UserDTO>> ThayDoiThongTin(Request_UpdateInfor request);
    }
}
