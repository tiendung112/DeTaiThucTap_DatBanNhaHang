using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.LoaiMonAn;
using DatBanNhaHang.Services.Implements;
using DatBanNhaHang.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace DatBanNhaHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiMonAnController : ControllerBase
    {

        private readonly ILoaiMonAn services;
        private readonly LoaiMonAn lma;
        private readonly MonAn monAn;

        public LoaiMonAnController( )
        {
            services = new LoaiMonAnServices();
            lma = new LoaiMonAn();
            monAn = new MonAn();
        }

        [HttpPost]
        [Route("/api/LoaiMonAn/ThemLoaiMonAn")]
        public async Task<IActionResult> ThemLoaiMonAn([FromBody] Request_ThemLoaiMonAn request)
        {
            var result = await services.ThemLoaiMonAn(request);
            if(result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("/api/LoaiMonAn/ThemLoaiMonAnKemMonAn")]
        public async Task<IActionResult> ThemLoaiMonAnKemMonAn([FromBody] Request_ThemLoaiMonAn request)
        {
            var result = await services.ThemLoaiMonAnKemMonAn(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        [Route("/api/LoaiMonAn/SuaLoaiMonAn")]
        [Authorize(Roles ="ADMIN,MOD")]
        public async Task<IActionResult> SuaLoaiMonAn([FromBody] Request_SuaLoaiMonAn request)
        {
            var result = await services.SuaLoaiMonAn(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("/api/LoaiMonAn/XoaLoaiMonAn")]
        [Authorize(Roles = "ADMIN,MOD")]
        public async Task<IActionResult> XoaLoaiMonAn([FromBody] Request_XoaLoaiMonAn request)
        {
            var result = await services.XoaLoaiMonAn(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("/api/LoaiMonAn/HienThiLoaiMonAn")]
        
        public async Task<IActionResult> HienThiLoaiMonAn(int pageSize , int pageNumber) 
        {
            return Ok("ok");
        }

    }
}
