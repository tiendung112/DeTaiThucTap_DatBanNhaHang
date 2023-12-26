using DatBanNhaHang.Entities.NguoiDung;
using DatBanNhaHang.Handler.Email;
using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.DTOs.NguoiDung;
using DatBanNhaHang.Payloads.Requests.NguoiDung.User;
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
        Task<PageResult<UserDTO>> GetAlls(int id ,int pageSize, int pageNumber);
        Task<ResponseObject<UserDTO>> XoaTaiKhoan(int id);
        Task<ResponseObject<UserDTO>> ChangePassword(int UserID, Request_ChangePassword request);
        string SendEmail(EmailTo emailTo);
        Task<ResponseObject<UserDTO>> ForgotPassword(Request_ForgotPassword request);
        Task<ResponseObject<UserDTO>> CreateNewPassword(Request_ConfirmCreateNewPassword request);
        Task<ResponseObject<UserDTO>> XacNhanDangKyTaiKhoan(Request_ValidateRegister request);
        Task<ResponseObject<UserDTO>> ThayDoiThongTin(int id, Request_UpdateInfor request);
        Task<string> XacNhanDoiEmail(Request_NewMail request);
        Task<string> ThayDoiEmail(int id);

    }
}
