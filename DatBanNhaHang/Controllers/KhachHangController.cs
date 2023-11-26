using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.KhachHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.MonAn;
using DatBanNhaHang.Services.Implements;
using DatBanNhaHang.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatBanNhaHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly IKhachHang services;
        private readonly KhachHang khachHang;
        public KhachHangController()
        {
            services = new KhachHangServices();
            khachHang = new KhachHang();
        }

        [HttpGet]
        [Route("/api/KhachHang/HienThiKhachHang")]
        //[Authorize(Roles = "ADMIN,MOD")]
        public async Task<IActionResult> HienThiKhachHang(int pageSize, int pageNumber)
        {
            return Ok(await services.HienThiKhachHang(0, pageSize, pageNumber));
        }

        [HttpGet]
        [Route("/api/KhachHang/HienThiKhachHang/{id}")]
        //[Authorize(Roles = "ADMIN,MOD")]
        public async Task<IActionResult> HienThiKhachHang([FromRoute]int id , int pageSize, int pageNumber)
        {
            return Ok(await services.HienThiKhachHang(id,0,0));
        }

        [HttpGet]
        [Route("/api/KhachHang/TimKiemKhachHangSDT")]
        //[Authorize(Roles = "ADMIN,MOD")]
        public async Task<IActionResult> TimKiemKhachHangSDT(string SDT)
        {
            return Ok(await services.TimKiemKhachHangSDT(SDT));
        }

        [HttpGet]
        [Route("/api/KhachHang/TimKiemKhachHangHoTen")]
        //[Authorize(Roles = "ADMIN,MOD")]
        public async Task<IActionResult> TimKiemKhachHangHoTen(string HoTen, int pageSize, int pageNumber)
        {
            return Ok(await services.TimKiemKhachHangHoTen(HoTen,pageSize,pageNumber));
        }

        [HttpPost]
        [Route("/api/KhachHang/ThemKhachHang")]
        //[Authorize(Roles ="ADMIN,MOD")]
        public async Task<IActionResult> ThemKhachHang([FromBody] Request_ThemKhachHang request)
        {
            var result = await services.ThemKhachHang(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        [Route("/api/KhachHang/SuaKhachHang/{id}")]
        //[Authorize(Roles = "ADMIN,MOD")]
        public async Task<IActionResult> SuaKhachHang([FromRoute]int id, [FromBody] Request_SuaKhachHang request)
        {
            var result = await services.SuaKhachHang(id,request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete]
        [Route("/api/KhachHang/XoaKhachHang/{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> XoaKhachHang([FromRoute]int id  )
        {
            var result = await services.XoaKhachHang(id);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
