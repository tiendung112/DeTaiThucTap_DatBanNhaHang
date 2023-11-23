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
        public async Task<IActionResult> ThemMonAn([FromBody] Request_ThemMonAn request)
        {
            var result = await services.ThemMonAn(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut]
        [Route("/api/MonAn/SuaMonAn/{id}")]
        public async Task<IActionResult> SuaMonAn([FromRoute] int id , [FromBody] Request_SuaMonAn request)
        {
            var result = await services.SuaMonAn(id,request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete]
        [Route("/api/MonAn/XoaMonAn/{id}")]
        public async Task<IActionResult> XoaMonAn([FromRoute] int id )
        {
            var result = await services.XoaMonAn(id);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("/api/MonAn/HienThiMonAn")]
        public async Task<IActionResult> HienThiMonAn( int pageSize, int pageNumber)
        {
            return Ok(await services.HienThiMonAn(0,pageSize,pageNumber));
        }

        [HttpGet]
        [Route("/api/MonAn/HienThiMonAn/{id}")]
        public async Task<IActionResult> HienThiMonAn(int id)
        {
            return Ok(await services.HienThiMonAn(id, 0,0 ));
        }

        [HttpGet]
        [Route("/api/MonAn/TimKiemMonAn/{tenMonAn}")]
        public async Task<IActionResult> TimKiemMonAn([FromRoute] string tenMonAn)
        {
            return Ok(await services.TimKiemMonAn(tenMonAn,0,0));
        }
    }
}
