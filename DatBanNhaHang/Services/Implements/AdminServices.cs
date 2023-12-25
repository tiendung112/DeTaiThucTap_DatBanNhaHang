using DatBanNhaHang.Entities.NguoiDung;
using DatBanNhaHang.Handler.Email;
using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.Converters.NguoiDung;
using DatBanNhaHang.Payloads.DTOs.NguoiDung;
using DatBanNhaHang.Payloads.Requests.NguoiDung;
using DatBanNhaHang.Payloads.Requests.NguoiDung.Admin;
using DatBanNhaHang.Payloads.Responses;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;
using DatBanNhaHang.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BCryptNet = BCrypt.Net.BCrypt;

namespace DatBanNhaHang.Services.Implements
{
    public class AdminServices : BaseService, IAdminServices
    {
        private readonly IConfiguration configuration;
        private readonly ResponseObject<AdminDTOs> responseObject;
        private readonly ResponseObject<TokenDTO> responseToken;
        private readonly AdminConverters converters;
        public AdminServices(IConfiguration _configuration)
        {
            configuration = _configuration;
            responseObject = new ResponseObject<AdminDTOs>();
            responseToken = new ResponseObject<TokenDTO>();
            converters = new AdminConverters();
        }

        #region Xử lý đăng ký và xác nhận đăng ký tài khoản
        public async Task<ResponseObject<AdminDTOs>> RegisterRequest(int id, Request_AdminRegister request)
        {
            if (string.IsNullOrWhiteSpace(request.Name)
               || string.IsNullOrWhiteSpace(request.password)
               || string.IsNullOrWhiteSpace(request.AdminName)
               || string.IsNullOrWhiteSpace(request.Email)
               || string.IsNullOrWhiteSpace(request.Gender)
               || string.IsNullOrWhiteSpace(request.QueQuan)
               || string.IsNullOrWhiteSpace(request.SDT))
            {
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Bạn cần truyền vào đầy đủ thông tin", null);
            }

            if (Validate.IsValidEmail(request.Email) == false)
            {
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Định dạng Email không hợp lệ", null);
            }

            if (await contextDB.User.FirstOrDefaultAsync(x => x.UserName.Equals(request.AdminName)) != null)
            {
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Tên tài khoản đã tồn tại trên hệ thống", null);
            }
            if (await contextDB.User.FirstOrDefaultAsync(x => x.Email.Equals(request.Email)) != null)
            {
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Email đã tồn tại trên hệ thống", null);
            }
            else
            {
                Admin admin = new Admin()
                {
                    AdminName = request.AdminName,
                    Email = request.Email,
                    create_at = DateTime.Now,
                    Gender = request.Gender,
                    Name = request.Name,
                    ngaysinh = request.ngaysinh,
                    password = BCryptNet.HashPassword(request.password),
                    SDT = request.SDT,
                    QueQuan = request.QueQuan,
                    ParentID = id,
                    RoleID = request.RoleID,
                    status = 1,
                };
                await contextDB.Admin.AddAsync(admin);
                await contextDB.SaveChangesAsync();
                return responseObject.ResponseSuccess("thêm tài khoản thành công ", converters.EntityToDTOs(admin));
            }
        }
        #endregion

        #region Xử lý việc đăng nhập và GenerateToken
        public TokenDTO GenerateAccessToken(Admin adm)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:SecretKey").Value!);

            var decentralization = contextDB.Role.FirstOrDefault(x => x.id == adm.RoleID);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", adm.id.ToString()),
                    new Claim(ClaimTypes.Email, adm.Email),
                    new Claim("AdminName", adm.AdminName),
                    new Claim("HoTen",adm.Name),
                    new Claim("SDT",adm.SDT),
                    //new Claim("Avatar", user.AvatarUrl),
                    new Claim("Roleid", adm.RoleID.ToString()),
                    new Claim(ClaimTypes.Role,decentralization?.RoleName ?? "")
                }),
                Expires = DateTime.UtcNow.AddDays(90),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            RefreshToken rf = new RefreshToken
            {
                Token = refreshToken,
                ExpiredTime = DateTime.UtcNow.AddDays(91),
                AdminID = adm.id
            };

            contextDB.RefreshToken.Add(rf);
            contextDB.SaveChanges();

            TokenDTO tokenDTO = new TokenDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return tokenDTO;
        }

        public string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }
        public async Task<ResponseObject<TokenDTO>> Login(Request_AdminLogin request)
        {
            if (string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.UserName))
            {
                return responseToken.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đầy đủ thông tin", null);
            }

            var admin = await contextDB.Admin.FirstOrDefaultAsync(x => x.AdminName.Equals(request.UserName));
            if (admin is null)
            {
                return responseToken.ResponseError(StatusCodes.Status404NotFound, "Tên tài khoản không tồn tại", null);
            }
            bool isPasswordValId = BCryptNet.Verify(request.Password, admin.password);
            if (!isPasswordValId)
            {
                return responseToken.ResponseError(StatusCodes.Status400BadRequest, "Tên đăng nhập hoặc mật khẩu không chính xác", null);
            }

            else
            {
                return responseToken.ResponseSuccess("Đăng nhập thành công", GenerateAccessToken(admin));
            }
        }
        public ResponseObject<TokenDTO> RenewAccessToken(TokenDTO request)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:SecretKey").Value);

            var tokenValIdation = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:SecretKey").Value))
            };

            try
            {
                var tokenAuthentication = jwtTokenHandler.ValidateToken(request.AccessToken, tokenValIdation, out var valIdatedToken);
                if (valIdatedToken is not JwtSecurityToken jwtSecurityToken || jwtSecurityToken.Header.Alg != SecurityAlgorithms.HmacSha256)
                {
                    return responseToken.ResponseError(StatusCodes.Status400BadRequest, "Token không hợp lệ", null);
                }
                RefreshToken refreshToken = contextDB.RefreshToken.FirstOrDefault(x => x.Token == request.RefreshToken);
                if (refreshToken == null)
                {
                    return responseToken.ResponseError(StatusCodes.Status404NotFound, "RefreshToken không tồn tại trong database", null);
                }
                if (refreshToken.ExpiredTime < DateTime.Now)
                {
                    return responseToken.ResponseError(StatusCodes.Status401Unauthorized, "Token chưa hết hạn", null);
                }

                var admin = contextDB.Admin.FirstOrDefault(x => x.id == refreshToken.AdminID);
                if (admin == null)
                {
                    return responseToken.ResponseError(StatusCodes.Status404NotFound, "Người dùng không tồn tại", null);
                }
                var newToken = GenerateAccessToken(admin);

                return responseToken.ResponseSuccess("Làm mới token thành công", newToken);
            }
            catch (Exception ex)
            {
                return responseToken.ResponseError(StatusCodes.Status500InternalServerError, ex.Message, null);
            }
        }
        #endregion

        /* #region Xử lý vấn đề liên quan đến gửi email
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
         #endregion*/
        #region Xử lý việc đổi mật khẩu và quên mật khẩu
        public async Task<ResponseObject<AdminDTOs>> ChangePassword(int AdminID, Request_AdminChangePassword request)
        {
            var admin = await contextDB.Admin.FirstOrDefaultAsync(x => x.id == AdminID);
            if (!BCryptNet.Verify(request.OldPassword, admin.password))
            {
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Mật khẩu cũ không chính xác", null);
            }
            admin.password = BCryptNet.HashPassword(request.NewPassword);
            contextDB.Admin.Update(admin);
            await contextDB.SaveChangesAsync();
            return responseObject.ResponseSuccess("Đối mật khẩu thành công", converters.EntityToDTOs(admin));

        }

        public async Task<ResponseObject<AdminDTOs>> CreateNewPassword(Request_AdminConfirmCreateNewPassword request)
        {
            XacNhanEmail confirmEmail = await contextDB.XacNhanEmail.Where(x => x.MaXacNhan.Equals(request.CodeActive)).FirstOrDefaultAsync();
            if (confirmEmail is null)
            {
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Mã xác nhận không chính xác", null);
            }
            if (confirmEmail.ThoiGianHetHan < DateTime.Now)
            {
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Mã xác nhận đã hết hạn", null);
            }
            Admin admin = contextDB.Admin.FirstOrDefault(x => x.id == confirmEmail.AdminID);
            admin.password = BCryptNet.HashPassword(request.NewPassword);
            contextDB.XacNhanEmail.Remove(confirmEmail);
            contextDB.Admin.Update(admin);
            await contextDB.SaveChangesAsync();
            return responseObject.ResponseSuccess("Tạo mật khẩu mới thành công", converters.EntityToDTOs(admin));
        }

        public async Task<ResponseObject<AdminDTOs>> ForgotPassword(Request_AdminForgotPassword request)
        {
            Admin admin = await contextDB.Admin.FirstOrDefaultAsync(x => x.Email.Equals(request.Email));
            if (admin is null)
            {
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Email không tồn tại ", null);
            }
            else
            {
                var confirms = contextDB.XacNhanEmail.Where(x => x.AdminID == admin.id).ToList();
                contextDB.XacNhanEmail.RemoveRange(confirms);
                await contextDB.SaveChangesAsync();
                XacNhanEmail confirmEmail = new XacNhanEmail
                {
                    AdminID = admin.id,
                    DaXacNhan = false,
                    ThoiGianHetHan = DateTime.Now.AddMinutes(15),
                    MaXacNhan = GenerateCodeActive().ToString()
                };
                await contextDB.XacNhanEmail.AddAsync(confirmEmail);
                await contextDB.SaveChangesAsync();
                string message = SendEmail(new EmailTo
                {
                    Mail = request.Email,
                    Subject = "Nhận mã xác nhận để tạo mật khẩu mới từ đây: ",
                    Content = $"Mã kích hoạt của bạn là: {confirmEmail.MaXacNhan}, mã này sẽ hết hạn sau 15 phút"
                });
                return responseObject.ResponseSuccess("Gửi mã xác nhận về email thành công, vui lòng kiểm tra email", null);
            }
        }
        #endregion

        #region hiển thị các tài khoản
        public async Task<PageResult<AdminDTOs>> GetAlls(int pageSize, int pageNumber)
        {
            var list = contextDB.Admin.Where(y=>y.status==1).Select(x => converters.EntityToDTOs(x));
            var result = Pagintation.GetPagedData<AdminDTOs>(list, pageSize, pageNumber);
            return result;
        }

        public async Task<PageResult<AdminDTOs>> XoaTaiKhoan(int id)
        {
            var acc = contextDB.Admin.SingleOrDefault(x => x.id == id);
            acc.status = 2;
            contextDB.Update(acc);
            await contextDB.SaveChangesAsync();
            var result = contextDB.Admin.Where(x=>x.id==id&&x.status==2).Select(y=>converters.EntityToDTOs(y));
            return  Pagintation.GetPagedData<AdminDTOs>(result,0,0);
        }

        public IQueryable<AdminDTOs> GetAdminTheoId(int id)
        {
            return contextDB.Admin.Where(x => x.id == id&&x.status==1).Select(y => converters.EntityToDTOs(y));
        }
        #endregion
        #region Xử lý việc thay đổi quyền hạn của người dùng và xoá tài khoản chưa active

        public string RemoveTKNotActive()
        {
            var lstUSer = contextDB.User.Where(x => x.IsActive == false);
            foreach (var t in lstUSer)
            {
                DateTime? next15P = t.ngayTao + TimeSpan.FromMinutes(15);
                if (t.ngayTao < DateTime.Now)
                {
                    t.status = 2;
                    contextDB.Update(t);
                }
            }
            contextDB.SaveChanges();
            return "đã xoá hết tài khoản chưa kích hoạt";
        }

        public async Task<ResponseObject<AdminDTOs>> ThayDoiQuyenHan(Request_AdminThayDoiQuyen request)
        {
            var admin = await contextDB.Admin.FirstOrDefaultAsync(x => x.id == request.AdminID);

            if (admin == null)
            {
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Tài khoản không tồn tại", null);
            }
            try
            {
                admin.RoleID = request.RoleID;
                contextDB.Admin.Update(admin);
                await contextDB.SaveChangesAsync();
                return responseObject.ResponseSuccess("Thay đổi quyền tài khoản thành công", converters.EntityToDTOs(admin));
            }
            catch (Exception ex)
            {
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Lỗi trong quá trình thay đổi quyền tài khoản", null);
            }
        }
        public async Task<ResponseObject<AdminDTOs>> SuaThongTin(int id, Request_AdminUpdateInfor request)
        {
            var admin = contextDB.Admin.SingleOrDefault(x => x.id == id);
            if (admin == null)
            {
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "không tồn tại tài khoản này", null);
            }
            if (await contextDB.User.FirstOrDefaultAsync(x => x.Email.Equals(request.Email)) != null)
            {
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Email đã tồn tại trên hệ thống", null);
            }
            if (Validate.IsValidEmail(request.Email) == false)
            {
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Định dạng Email không hợp lệ", null);
            }

            admin.Email = request.Email ?? admin.Email; //: request.Email;
            admin.QueQuan = request.QueQuan ??admin.QueQuan ;//: request.QueQuan;
            admin.SDT = request.SDT ?? admin.SDT; ///: request.SDT;
            admin.Name = request.Name  ?? admin.Name; //: request.Name;
            admin.ngaysinh = request.ngaysinh ?? admin.ngaysinh; //: request.ngaysinh;
            admin.Gender = request.gioiTinh?? admin.Gender;// : request.gioiTinh;
            contextDB.Update(admin);
            await contextDB.SaveChangesAsync();
            return responseObject.ResponseSuccess("đổi thông tin thành công", converters.EntityToDTOs(admin));
        }
        #endregion

    }
}
