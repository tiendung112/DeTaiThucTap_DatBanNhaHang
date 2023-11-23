using DatBanNhaHang.Payloads.Requests.NhaHang.Ban;
using DatBanNhaHang.Services.Implements;
using DatBanNhaHang.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace DatBanNhaHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanController : ControllerBase
    {
        private readonly IBan services;
        public BanController()
        {
            services = new BanServices();
        }

        #region hiển thị , tìm kiếm bàn
        [HttpGet]
        [Route("/api/Ban/HienThiBan/{id}")]
        public async Task<IActionResult> HienThiBan([FromRoute] int id, int pageSize, int pageNumber)
        {
            return Ok(await services.HienThiBan(id, pageSize, pageNumber));
        }


        [HttpGet]
        [Route("/api/Ban/HienThiBanTheoTrangThai")]
        public async Task<IActionResult> HienThiBanTheoTrangThai(int pageSize, int pageNumber)
        {
            return Ok(await services.HienThiBanTheoTrangThai(pageSize, pageNumber));
        }

        [HttpGet]
        [Route("/api/Ban/HienThiBanTheoViTri")]
        public async Task<IActionResult> HienThiBanTheoViTri(int pageSize, int pageNumber)
        {
            return Ok(await services.HienThiBanTheoViTri(pageSize, pageNumber));
        }

        [HttpGet]
        [Route("/api/Ban/HienThiBanTheoLoaiBan/{lbid}")]
        public async Task<IActionResult> HienThiBanTheoLoaiBan([FromRoute] int lbid, int pageSize, int pageNumber)
        {
            return Ok(await services.HienThiBanTheoLoaiBan(lbid, pageSize, pageNumber));
        }
        #endregion
        #region thêm sửa xoá bàn 
        [HttpPost]
        [Route("/api/Ban/ThemBan")]
        //[Authorize(Roles = "ADMIN,MOD")]
        public async Task<IActionResult> ThemBan([FromBody] Request_ThemBan request)
        {
            var result = await services.ThemBan(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }

        [HttpPut]
        [Route("/api/Ban/SuaBan{id}")]
        //[Authorize(Roles = "ADMIN,MOD")]
        public async Task<IActionResult> SuaBan([FromRoute] int id, [FromBody] Request_SuaBan request)
        {
            var result = await services.SuaBan(id, request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("/api/Ban/XoaBan{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> XoaBan([FromRoute] int id)
        {
            var result = await services.XoaBan(id);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        #endregion
    }
}