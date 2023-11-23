using DatBanNhaHang.Payloads.Requests.NhaHang.LoaiBan;
using DatBanNhaHang.Services.Implements;
using DatBanNhaHang.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace DatBanNhaHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiBanController : ControllerBase
    {
        public ILoaiBan services;
        public LoaiBanController()
        {
            services = new LoaiBanServices();
        }

        #region thêm sửa xoá bàn 
        [HttpPost]
        [Route("api/LoaiBan/ThemLoaiBan")]
        //[Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> ThemLoaiBan([FromBody] Request_ThemLoaiBan request)
        {
            var result = await services.ThemLoaiBan(request);
            if (result != null)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut]
        [Route("api/LoaiBan/SuaLoaiBan/{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> SuaLoaiBan([FromRoute] int id, [FromBody] Request_SuaLoaiBan request)
        {
            var result = await services.SuaLoaiBan(id, request);
            if (result != null)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete]
        [Route("api/LoaiBan/XoaLoaiBan/{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> XoaLoaiBan([FromRoute] int id )
        {
            var result = await services.XoaLoaiBan(id);
            if (result != null)
                return Ok(result);
            return BadRequest(result);
        }
        #endregion
        #region hiển thị , tìm kiếm

        [HttpGet]
        [Route("api/LoaiBan/HienThiLoaiBan")]
        public async Task<IActionResult> HienThiLoaiBan(int pageSize, int pageNumber)
        {
            return Ok(await services.HienThiLoaiBan(0,pageSize,pageNumber));
        }
        [HttpGet]
        [Route("api/LoaiBan/HienThiLoaiBan/{id}")]
        public async Task<IActionResult> HienThiLoaiBan(int id)
        {
            return Ok(await services.HienThiLoaiBan(id, 0, 0));
        }
        #endregion
    }
}
