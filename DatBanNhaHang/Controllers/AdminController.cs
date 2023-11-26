using DatBanNhaHang.Payloads.DTOs.NguoiDung;
using DatBanNhaHang.Payloads.Requests.NguoiDung;
using DatBanNhaHang.Payloads.Requests.NguoiDung.Admin;
using DatBanNhaHang.Services.Implements;
using DatBanNhaHang.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatBanNhaHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices services;
        private readonly IConfiguration _configuration;
        public AdminController(IConfiguration configuration , IAdminServices _services)
        {
            services =_services;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("/api/Admin/register")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Register([FromForm] Request_AdminRegister register)
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirst("Id").Value);
            var result = await services.RegisterRequest(id,register);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route(("/api/Admin/login"))]
        public async Task<IActionResult> Login(Request_AdminLogin request)
        {
            var result = await services.Login(request);
            if (result == null)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("/api/Admin/renew-token")]
        public IActionResult RenewToken(TokenDTO token)
        {
            var result = services.RenewAccessToken(token);
            if (result == null)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("/api/Admin/get-all")]
        [Authorize]
        public async Task<IActionResult> GetAlls(int pageSize, int pageNumber)
        {
            return Ok(await services.GetAlls(pageSize, pageNumber));
        }

        [HttpPut]
        [Route("/api/Admin/change-password")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword(Request_AdminChangePassword request)
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirst("Id").Value);
            return Ok(await services.ChangePassword(id, request));

        }
        [HttpPost]
        [Route("/api/Admin/forgot-password")]
        public async Task<IActionResult> ForgotPassword(Request_AdminForgotPassword request)
        {
            return Ok(await services.ForgotPassword(request));
        }

        [HttpPost]
        [Route("/api/Admin/create-new-password")]
        public async Task<IActionResult> CreateNewPassword(Request_AdminConfirmCreateNewPassword request)
        {
            return Ok(await services.CreateNewPassword(request));
        }


        [HttpDelete]
        [Route("/api/Admin/XoaTKChuaKichHoat")]
        //[Authorize(Roles = "ADMIN,MOD")]
        public IActionResult DeleteTK()
        {
            var res = services.RemoveTKNotActive();
            return Ok(res);

        }

        [HttpPut]
        [Route("/api/Admin/ThayDoiQuyenHan")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ThayDoiQuyenHan([FromBody] Request_AdminThayDoiQuyen request)
        {
            return Ok(await services.ThayDoiQuyenHan(request));
        }
    }
}
