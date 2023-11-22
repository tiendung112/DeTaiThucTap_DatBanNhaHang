using DatBanNhaHang.Payloads.Requests.NhaHang.TrangThaiBan;
using DatBanNhaHang.Services.Implements;
using DatBanNhaHang.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatBanNhaHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrangThaiBanController : ControllerBase
    {
        private readonly ITrangThaiBan services;
        public TrangThaiBanController()
        {
            services = new TrangThaiBanServices();
        }

        [HttpPost]
        [Route("/api/TrangThaiBan/ThemTrangThaiBan")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ThemTrangThaiBan([FromBody] Request_ThemTrangThaiBan request)
        {
            var result = await services.ThemTrangThaiBan(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        [Route("/api/TrangThaiBan/SuaTrangThaiBan")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> SuaTrangThaiBan([FromBody] Request_SuaTrangThaiBan request)
        {
            var result = await services.SuaTrangThaiBan(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("/api/TrangThaiBan/XoaTrangThaiBan")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> XoaTrangThaiBan([FromBody] Request_XoaTrangThaiBan request)
        {
            var result = await services.XoaTrangThaiBan(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("/api/TrangThaiBan/HienThiTrangThaiBan")]
        [Authorize(Roles = "ADMIN , MOD")]
        public async Task<IActionResult> HienThiTrangThaiBan()
        {
            var result = await services.HienThiTrangThaiBan();
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
