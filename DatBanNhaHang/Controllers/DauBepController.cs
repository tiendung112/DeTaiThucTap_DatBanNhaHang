using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Handler.Pagination;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NguoiDung;
using DatBanNhaHang.Payloads.Requests.NhaHang.DauBep;
using DatBanNhaHang.Services.Implements;
using DatBanNhaHang.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.WebApi;

namespace DatBanNhaHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DauBepController : ControllerBase
    {
        private readonly IDauBep services;
        private readonly DauBep db;
        public DauBepController(  IDauBep dauBepService)
        {
            services = dauBepService;
            db = new DauBep();
        }

        [HttpPost]
        [Route("/api/DauBep/ThemDauBep")]
        //[Authorize(Roles ="ADMIN,MOD")]
        public async Task<IActionResult> ThemDauBep([FromBody] Request_ThemDauBep request)
        {
            var result = await services.ThemDauBep(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        [Route("/api/DauBep/SuaDauBep/{id}")]
        //[Authorize(Roles = "ADMIN , MOD")]
        public async Task<IActionResult> SuaDauBep(int id ,[FromBody] Request_SuaDauBep request)
        {
            var result =await services.SuaDauBep(id,request);
            
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete]
        [Route("/api/DauBep/XoaDauBep/{id}")]
        //[Authorize(Roles ="ADMIN , MOD")]
        public async Task<IActionResult> XoaDauBep([FromRoute]int id )
        {
            var result = await services.XoaDauBep(id);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("/api/DauBep/HienThiDanhSachDauBep")]
        public async Task<IActionResult> LayDSDauBep( int pageSize, int pageNumber)
        {
            int id = 0;
            return Ok(await services.GetDSDauBep(id, pageSize, pageNumber));
        }
        [HttpGet]
        [Route("/api/DauBep/HienThiDanhSachDauBep/{id}")]
        public async Task<IActionResult> LayDSDauBepTheoID([FromRoute]int id)
        {
            return Ok(await services.GetDSDauBep(id, 0, 0));
        }
    }
}
