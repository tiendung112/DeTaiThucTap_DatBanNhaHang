using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.LoaiMonAn;
using DatBanNhaHang.Payloads.Requests.NhaHang.MonAn;
using DatBanNhaHang.Services.Implements;
using DatBanNhaHang.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatBanNhaHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonAnController : ControllerBase
    {

        private readonly IMonAn services;
        private readonly MonAn monAn;

        public MonAnController()
        {
            services = new MonAnServices();
            monAn = new MonAn();
        }

        [HttpPost]
        [Route("/api/MonAn/ThemMonAn")]
        public async Task<IActionResult> ThemMonAn([FromForm] Request_ThemMonAn request)
        {
            var result = await services.ThemMonAn(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut]
        [Route("/api/MonAn/SuaMonAn")]
        public async Task<IActionResult> SuaMonAn([FromForm] Request_SuaMonAn request)
        {
            var result = await services.SuaMonAn(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete]
        [Route("/api/MonAn/XoaMonAn")]
        public async Task<IActionResult> XoaMonAn([FromBody] Request_XoaMonAn request)
        {
            var result = await services.XoaMonAn(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }



        [HttpGet]
        [Route("/api/MonAn/HienThiMonAn")]
        public async Task<IActionResult> HienThiMonAn(int pageSize, int pageNumber)
        {
            return Ok("ok");
        }

        [HttpGet]
        [Route("/api/MonAn/TimKiemMonAn")]
        public async Task<IActionResult> TimKiemMonAn([FromBody] Request_TimKiemMonAn request,int pageSize, int pageNumber)
        {
            return Ok("ok");
        }
    }
}
