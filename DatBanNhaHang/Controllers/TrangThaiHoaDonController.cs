using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.LoaiMonAn;
using DatBanNhaHang.Payloads.Requests.NhaHang.TrangThaiHoaDon;
using DatBanNhaHang.Services.Implements;
using DatBanNhaHang.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatBanNhaHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrangThaiHoaDonController : ControllerBase
    {
        private readonly ITrangThaiHoaDon services;
        

        public TrangThaiHoaDonController()
        {
            services = new TrangThaiHoaDonServices();
            
        }

        [HttpPost]
        [Route("/api/TrangThaiHoaDon/ThemTrangThaiHoaDon")]
        [Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> ThemTrangThaiHoaDon([FromBody]Request_ThemTrangThaiHoaDon request)
        {
            var result = await services.ThemTrangThaiHoaDon(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut]
        [Route("/api/TrangThaiHoaDon/SuaTrangThaiHoaDon")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> SuaTrangThaiHoaDon([FromBody] Request_SuaTrangThaiHoaDon request)
        {
            var result = await services.SuaTrangThaiHoaDon(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete]
        [Route("/api/TrangThaiHoaDon/XoaTrangThaiHoaDon")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> XoaTrangThaiHoaDon([FromBody] Request_XoaTrangThaiHoaDon request)
        {
            var result = await services.XoaTrangThaiHoaDon(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("/api/TrangThaiHoaDon/HienThiTrangThaiHoaDon")]
        [Authorize(Roles = "ADMIN , MOD")]
        public async Task<IActionResult> HienThiTrangThaiHoaDon()
        {
            var result = await services.HienThiTrangThaiHoaDon();
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
