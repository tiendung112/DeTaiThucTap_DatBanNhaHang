using DatBanNhaHang.Entities;
using DatBanNhaHang.Handler.Email;
using DatBanNhaHang.Handler.Image;
using DatBanNhaHang.Payloads.Responses;
using DatBanNhaHang.Services.IServices;
using AutoMapper;
using Azure.Core;
using CloudinaryDotNet;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using BCryptNet = BCrypt.Net.BCrypt;
using SmtpClient = System.Net.Mail.SmtpClient;
using DatBanNhaHang.Services.Implements.DatBanNhaHang.Service.Implements;
using DatBanNhaHang.Entities.NguoiDung;
using DatBanNhaHang.Payloads.Requests.NguoiDung.User;
using DatBanNhaHang.Payloads.DTOs.NguoiDung;
using DatBanNhaHang.Payloads.Converters.NguoiDung;
using Azure;
using DatBanNhaHang.Entities.NguoiDung;
using DatBanNhaHang.Handler.Pagination;

namespace DatBanNhaHang.Services.Implements
{
    public class AuthServices : BaseService, IAuthServices
    {
        private readonly IConfiguration _configuration;
        private readonly ResponseObject<UserDTO> _responseObject;
        private readonly ResponseObject<TokenDTO> _responseObjectToken;
        private readonly UserConverters _userConverter;

        public AuthServices(
            IConfiguration configuration,
            ResponseObject<UserDTO> responseObject,
            ResponseObject<TokenDTO> reponseObjectToken,
            UserConverters userConverter
            )
        {
            _configuration = configuration;
            _responseObject = responseObject;
            _responseObjectToken = reponseObjectToken;
            _userConverter = userConverter;
        }
        #region Xử lý đăng ký và xác nhận đăng ký tài khoản
        public async Task<ResponseObject<UserDTO>> RegisterRequest(Request_Register request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName)
               || string.IsNullOrWhiteSpace(request.Password)
               /*|| string.IsNullOrWhiteSpace(request.FirstName)*/
               || string.IsNullOrWhiteSpace(request.Name)
               || string.IsNullOrWhiteSpace(request.Email))
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Bạn cần truyền vào đầy đủ thông tin", null);
            }

            if (Validate.IsValidEmail(request.Email) == false)
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Định dạng Email không hợp lệ", null);
            }

            if (await contextDB.User.FirstOrDefaultAsync(x => x.UserName.Equals(request.UserName)) != null)
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Tên tài khoản đã tồn tại trên hệ thống", null);
            }
            if (await contextDB.User.FirstOrDefaultAsync(x => x.Email.Equals(request.Email)) != null)
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Email đã tồn tại trên hệ thống", null);
            }
            else
            {
                //int imageSize = 2 * 1024 * 768;
                try
                {
                    User user = new User();
                    user.UserName = request.UserName;
                    user.Name = request.Name;
                    //user.FirstName = request.FirstName;
                    user.Password = BCryptNet.HashPassword(request.Password);
                    //user.LastName = request.LastName;
                    user.Email = request.Email;
                    user.DateOfBirth = request.DateOfBirth;
                    user.Gender = request.Gender;
                    //user.Roleid = 3;
                    user.ngayTao = DateTime.Now;
                    user.AvatarUrl = request.AvatarUrl;
                   /* if (request.AvatarUrl != null)
                    {
                        if (!HandleImage.IsImage(request.AvatarUrl, imageSize))
                        {
                            return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                        }
                        else
                        {
                            var avatarFile = await HandleUploadImage.Upfile(request.AvatarUrl, "DatBanNhaHang/Account");
                            user.AvatarUrl = avatarFile == "" ? "https://media.istockphoto.com/Id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=" : avatarFile;
                        }
                    }*/
                    await contextDB.User.AddAsync(user);
                    await contextDB.SaveChangesAsync();

                    var confirms = contextDB.XacNhanEmail.Where(x => x.UserID == user.id).ToList();
                    contextDB.XacNhanEmail.RemoveRange(confirms);
                    await contextDB.SaveChangesAsync();

                    XacNhanEmail confrimEmail = new XacNhanEmail()
                    {
                        UserID = user.id,
                        DaXacNhan = false,
                        ThoiGianHetHan = DateTime.Now.AddMinutes(30),
                        MaXacNhan = GenerateCodeActive().ToString()
                    };
                    await contextDB.XacNhanEmail.AddAsync(confrimEmail);
                    await contextDB.SaveChangesAsync();
                    string message = SendEmail(new EmailTo
                    {
                        Mail = request.Email,
                        Subject = "Nhận mã xác nhận để đăng ký tài khoản tại đây: ",
                        Content = $"Mã kích hoạt của bạn là: {confrimEmail.MaXacNhan}" +
                        $", mã này sẽ hết hạn sau 30 phút"
                    });
                    return _responseObject.ResponseSuccess
                        ("Bạn đã gửi yêu cầu đăng ký tài khoản," +
                        " vui lòng nhập mã xác nhận đã được gửi về email của bạn"
                        , _userConverter.EntityToDTO(user));
                }
                catch (Exception ex)
                {
                    return _responseObject.ResponseError(StatusCodes.Status500InternalServerError, ex.Message, null);
                }
            }
        }
        public async Task<string> XacNhanDangKyTaiKhoan(Request_ValidateRegister request)
        {

            XacNhanEmail confirmEmail = await contextDB.XacNhanEmail.Where(x => x.MaXacNhan.Equals(request.MaXacNhan)).FirstOrDefaultAsync();
            if (confirmEmail is null)
            {
                return "Mã xác nhận không chính xác";
            }
            if (confirmEmail.ThoiGianHetHan < DateTime.Now)
            {
                return "Mã xác nhận đã hết hạn";
            }
            User user = contextDB.User.FirstOrDefault(x => x.id == confirmEmail.UserID);
            user.IsActive = true;
            user.ngayTao = DateTime.Now;
            contextDB.XacNhanEmail.Remove(confirmEmail);
            contextDB.User.Update(user);
            await contextDB.SaveChangesAsync();
            return "Xác nhận đăng ký tài khoản thành công, vui lòng đăng nhập tài khoản của bạn";
        }

        #endregion
        #region Xử lý việc đăng nhập và GenerateToken
        public TokenDTO GenerateAccessToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value!);

            //var decentralization = contextDB.Role.FirstOrDefault(x => x.id == user.Roleid);
            //var decentralization = contextDB.Role.FirstOrDefault(x => x.id == user.Roleid);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("Username", user.UserName),
                    new Claim("Avatar", user.AvatarUrl),
                    /*new Claim("Roleid", user.Roleid.ToString()),
                    new Claim(ClaimTypes.Role,decentralization?.RoleName ?? "")*/
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            RefreshToken rf = new RefreshToken
            {
                Token = refreshToken,
                ExpiredTime = DateTime.UtcNow.AddHours(5),
                UserID = user.id
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
        public async Task<ResponseObject<TokenDTO>> Login(Request_Login request)
        {
            if (string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.UserName))
            {
                return _responseObjectToken.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đầy đủ thông tin", null);
            }

            var user = await contextDB.User.FirstOrDefaultAsync(x => x.UserName.Equals(request.UserName));
            if (user is null)
            {
                return _responseObjectToken.ResponseError(StatusCodes.Status404NotFound, "Tên tài khoản không tồn tại", null);
            }
            else if (user.IsActive == false)
            {
                return _responseObjectToken.ResponseError(StatusCodes.Status400BadRequest, "Tài khoản chưa kích hoạt", null);
            }
            bool isPasswordValId = BCryptNet.Verify(request.Password, user.Password);
            if (!isPasswordValId)
            {
                return _responseObjectToken.ResponseError(StatusCodes.Status400BadRequest, "Tên đăng nhập hoặc mật khẩu không chính xác", null);
            }

            else
            {
                return _responseObjectToken.ResponseSuccess("Đăng nhập thành công", GenerateAccessToken(user));
            }
        }

        public ResponseObject<TokenDTO> RenewAccessToken(TokenDTO request)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value);

            var tokenValIdation = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value))
            };

            try
            {
                var tokenAuthentication = jwtTokenHandler.ValidateToken(request.AccessToken, tokenValIdation, out var valIdatedToken);
                if (valIdatedToken is not JwtSecurityToken jwtSecurityToken || jwtSecurityToken.Header.Alg != SecurityAlgorithms.HmacSha256)
                {
                    return _responseObjectToken.ResponseError(StatusCodes.Status400BadRequest, "Token không hợp lệ", null);
                }
                RefreshToken refreshToken = contextDB.RefreshToken.FirstOrDefault(x => x.Token == request.RefreshToken);
                if (refreshToken == null)
                {
                    return _responseObjectToken.ResponseError(StatusCodes.Status404NotFound, "RefreshToken không tồn tại trong database", null);
                }
                if (refreshToken.ExpiredTime < DateTime.Now)
                {
                    return _responseObjectToken.ResponseError(StatusCodes.Status401Unauthorized, "Token chưa hết hạn", null);
                }
                var user = contextDB.User.FirstOrDefault(x => x.id == refreshToken.UserID);
                if (user == null)
                {
                    return _responseObjectToken.ResponseError(StatusCodes.Status404NotFound, "Người dùng không tồn tại", null);
                }
                var newToken = GenerateAccessToken(user);

                return _responseObjectToken.ResponseSuccess("Làm mới token thành công", newToken);
            }
            catch (Exception ex)
            {
                return _responseObjectToken.ResponseError(StatusCodes.Status500InternalServerError, ex.Message, null);
            }
        }
        #endregion
        #region Xử lý vấn đề liên quan đến gửi email
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
        private int GenerateCodeActive()
        {
            Random random = new Random();
            return random.Next(100000, 999999);
        }
        #endregion
        #region Xử lý việc đổi mật khẩu và quên mật khẩu
        public async Task<ResponseObject<UserDTO>> ChangePassword(int UserID, Request_ChangePassword request)
        {
            var user = await contextDB.User.FirstOrDefaultAsync(x => x.id == UserID);
            if (!BCryptNet.Verify(request.OldPassword, user.Password))
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Mật khẩu cũ không chính xác", null);
            }
            user.Password = BCryptNet.HashPassword(request.NewPassword);
            contextDB.User.Update(user);
            await contextDB.SaveChangesAsync();
            return _responseObject.ResponseSuccess("Đối mật khẩu thành công", _userConverter.EntityToDTO(user));
        }
        public async Task<string> ForgotPassword(Request_ForgotPassword request)
        {
            User user = await contextDB.User.FirstOrDefaultAsync(x => x.Email.Equals(request.Email));
            if (user is null)
            {
                return "Email không tồn tại trong hệ thống";
            }
            else
            {
                var confirms = contextDB.XacNhanEmail.Where(x => x.UserID == user.id).ToList();
                contextDB.XacNhanEmail.RemoveRange(confirms);
                await contextDB.SaveChangesAsync();
                XacNhanEmail confirmEmail = new XacNhanEmail
                {
                    UserID = user.id,
                    DaXacNhan = false,
                    ThoiGianHetHan = DateTime.Now.AddMinutes(5),
                    MaXacNhan = GenerateCodeActive().ToString()
                };
                await contextDB.XacNhanEmail.AddAsync(confirmEmail);
                await contextDB.SaveChangesAsync();
                string message = SendEmail(new EmailTo
                {
                    Mail = request.Email,
                    Subject = "Nhận mã xác nhận để tạo mật khẩu mới từ đây: ",
                    Content = $"Mã kích hoạt của bạn là: {confirmEmail.MaXacNhan}, mã này sẽ hết hạn sau 5 phút"
                });
                return "Gửi mã xác nhận về email thành công, vui lòng kiểm tra email";
            }
        }
        public async Task<ResponseObject<UserDTO>> CreateNewPassword(Request_ConfirmCreateNewPassword request)
        {
            XacNhanEmail confirmEmail = await contextDB.XacNhanEmail.Where(x => x.MaXacNhan.Equals(request.CodeActive)).FirstOrDefaultAsync();
            if (confirmEmail is null)
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Mã xác nhận không chính xác", null);
            }
            if (confirmEmail.ThoiGianHetHan < DateTime.Now)
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Mã xác nhận đã hết hạn", null);
            }
            User user = contextDB.User.FirstOrDefault(x => x.id == confirmEmail.UserID);
            user.Password = BCryptNet.HashPassword(request.NewPassword);
            contextDB.XacNhanEmail.Remove(confirmEmail);
            contextDB.User.Update(user);
            await contextDB.SaveChangesAsync();
            return _responseObject.ResponseSuccess("Tạo mật khẩu mới thành công", _userConverter.EntityToDTO(user));

        }
        #endregion
        public async Task<PageResult<UserDTO>> GetAlls(int pageSize, int pageNumber)
        {
            var list = contextDB.User.Select(x => _userConverter.EntityToDTO(x));
            var result =Pagintation.GetPagedData<UserDTO>(list, pageSize, pageNumber);
            return result;
        }
        #region Xử lý việc thay đổi quyền hạn của người dùng và xoá tài khoản chưa active
        public async Task<string> ThayDoiQuyenHan(Request_ThayDoiQuyen request)
        {
            /*var NguoiDung = await contextDB.User.FirstOrDefaultAsync(x => x.id == request.UserID);

            if (NguoiDung == null)
            {
                return "Tài khoản không tồn tại";
            }

            NguoiDung.Roleid = request.RoleID;
            try
            {
                contextDB.User.Update(NguoiDung);
                await contextDB.SaveChangesAsync();
                return "Thay đổi quyền tài khoản thành công";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return "Lỗi trong quá trình thay đổi quyền tài khoản";
            }*/ return "Lỗi trong quá trình thay đổi quyền tài khoản";
        }
        public string RemoveTKNotActive()
        {
            var lstUSer = contextDB.User.Where(x => x.IsActive == false);
            foreach (var t in lstUSer)
            {
                DateTime? next15P = t.ngayTao + TimeSpan.FromMinutes(15);
                if (t.ngayTao < DateTime.Now)
                {
                    contextDB.Remove(t);
                }
            }
            contextDB.SaveChanges();
            return "đã xoá hết tài khoản chưa kích hoạt";
        }
        #endregion

        //public async Task<ResponseObject<UserDTO>> ThayDoiThongTin(Request_UpdateInfor request)
        //{
        //    var user = contextDB.User.SingleOrDefault(x => x.id == request.UserID);
        //    if(user == null) 
        //    {
        //        return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại tài khoản này ", null);
        //    }
        //    if (contextDB.User.Any(x => x.Email == request.Email && x.Email != user.Email))
        //    {
        //        return _responseObject.ResponseError(StatusCodes.Status404NotFound, "đã tồn tại email này ", null);
        //    }

        //}
    }
}