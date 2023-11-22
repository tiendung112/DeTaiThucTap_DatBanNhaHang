using DatBanNhaHang.Payloads.Requests.NhaHang.LoaiBan;
using DatBanNhaHang.Services.Implements;
using DatBanNhaHang.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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


        [HttpPost]
        [Route("api/LoaiBan/ThemLoaiBan")]
        [Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> ThemLoaiBan([FromBody] Request_ThemLoaiBan request)
        {
            var result = await services.ThemLoaiBan(request);
            if(result != null)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut]
        [Route("api/LoaiBan/SuaLoaiBan")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> SuaLoaiBan([FromBody] Request_SuaLoaiBan request)
        {
            var result = await services.SuaLoaiBan(request);
            if (result != null)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete]
        [Route("api/LoaiBan/XoaLoaiBan")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> XoaLoaiBan([FromBody] Request_XoaLoaiBan request)
        {
            var result = await services.XoaLoaiBan(request);
            if (result != null)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet]
        [Route("api/LoaiBan/HienThiLoaiBan")]
        public async Task<IActionResult> HienThiLoaiBan()
        {
            var result = await services.HienThiLoaiBan();
            if (result != null)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
