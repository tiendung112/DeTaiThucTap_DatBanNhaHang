using DatBanNhaHang.Entities.NguoiDung;
using DatBanNhaHang.Handler.Email;
using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.DTOs.NguoiDung;
using DatBanNhaHang.Payloads.Requests.NguoiDung;
using DatBanNhaHang.Payloads.Requests.NguoiDung.Admin;
using DatBanNhaHang.Payloads.Responses;

namespace DatBanNhaHang.Services.IServices
{
    public interface IAdminServices
    {
        string GenerateRefreshToken();
        TokenDTO GenerateAccessToken(Admin adm);
        ResponseObject<TokenDTO> RenewAccessToken(TokenDTO request);
        Task<ResponseObject<TokenDTO>> Login(Request_AdminLogin request);
        Task<ResponseObject<AdminDTOs>> RegisterRequest(int id , Request_AdminRegister request);
        Task<PageResult<AdminDTOs>> GetAlls(int pageSize, int pageNumber);
        Task<ResponseObject<AdminDTOs>> ChangePassword(int AdminID, Request_AdminChangePassword request);
        string SendEmail(EmailTo emailTo);
        Task<ResponseObject<AdminDTOs>> ForgotPassword(Request_AdminForgotPassword request);
        Task<ResponseObject<AdminDTOs>> CreateNewPassword(Request_AdminConfirmCreateNewPassword request);  
        string RemoveTKNotActive();
        Task<string> ThayDoiQuyenHan(Request_AdminThayDoiQuyen request);
        //Task<ResponseObject<UserDTO>> ThayDoiThongTin(Request_UpdateInfor request);
    }
}
