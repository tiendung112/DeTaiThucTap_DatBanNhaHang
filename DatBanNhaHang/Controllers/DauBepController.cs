using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Pagination;
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
        private readonly IConfiguration _configuration;
        private readonly IDauBep _dauBepService;
        private readonly DauBep db;
        public DauBepController(IConfiguration configuration, IDauBep dauBepService)
        {
            _configuration = configuration;
            _dauBepService = dauBepService;
            db = new DauBep();
        }


        [HttpPost]
        [Route("/api/DauBep/ThemDauBep")]
        //[Authorize(Roles ="ADMIN,MOD")]
        public async Task<IActionResult> ThemDauBep([FromForm] Request_ThemDauBep request)
        {
            var result = await _dauBepService.ThemDauBep(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        [Route("/api/DauBep/SuaDauBep")]
        [Authorize(Roles = "ADMIN , MOD")]
        public async Task<IActionResult> SuaDauBep([FromForm] Request_SuaDauBep request)
        {
            var result =await _dauBepService.SuaDauBep(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete]
        [Route("/api/DauBep/XoaDauBep")]
        [Authorize(Roles ="ADMIN , MOD")]
        public async Task<IActionResult> XoaDauBep([FromBody] Request_XoaDauBep request)
        {
            var result = await _dauBepService.XoaDauBep(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("/api/DauBep/LayDanhSachDauBep")]
        public async Task<IActionResult> LayDSDauBep(int pageSize, int pageNumber)
        {
            if(pageSize != 0 && pageNumber != 0)
            {
                Pagintation pagintation = new Pagintation()
                {
                    PageSize = pageSize,
                    PageNumber = pageNumber
                };

                var lstDB = await _dauBepService.GetDSDauBep(pagintation);

                var PTLstDb = PageResult<DauBepDTOs>.toPageResult(pagintation, lstDB);
                pagintation.TotalCount = lstDB.Count();

                var res = new PageResult<DauBepDTOs>(pagintation, PTLstDb);
                return Ok(res);
            }
            else
            {
                Pagintation pagintation = new Pagintation();
                return Ok(_dauBepService.GetDSDauBep(pagintation));
            }
           
        }
    }
}
