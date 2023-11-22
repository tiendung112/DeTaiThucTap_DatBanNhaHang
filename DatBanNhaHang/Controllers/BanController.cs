using DatBanNhaHang.Pagination;
using DatBanNhaHang.Payloads.DTOs.NhaHang;
using DatBanNhaHang.Payloads.Requests.NhaHang.Ban;
using DatBanNhaHang.Services.Implements;
using DatBanNhaHang.Services.IServices;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Route("/api/Ban/HienThiBan")]
        public async Task<IActionResult> HienThiBan(int pageSize, int pageNumber)
        {
            if (pageSize != 0 && pageNumber != 0)
            {
                Pagintation pagintation = new Pagintation()
                {
                    PageSize = pageSize,
                    PageNumber = pageNumber
                };

                var lstDB = await services.HienThiBan(pagintation);

                var PTLstDb = PageResult<BanDTOs>.toPageResult(pagintation, lstDB);
                pagintation.TotalCount = lstDB.Count();

                var res = new PageResult<BanDTOs>(pagintation, PTLstDb);
                return Ok(res);
            }
            else
            {
                Pagintation pagintation = new Pagintation();
                return Ok(services.HienThiBan(pagintation));
            }
        }


        [HttpGet]
        [Route("/api/Ban/HienThiBanTheoTrangThai")]
        public async Task<IActionResult> HienThiBanTheoTrangThai(int ttID, int pageSize, int pageNumber)
        {
            if (pageSize != 0 && pageNumber != 0)
            {
                Pagintation pagintation = new Pagintation()
                {
                    PageSize = pageSize,
                    PageNumber = pageNumber
                };

                var lstDB = await services.HienThiBanTheoTrangThai(ttID,pagintation);

                var PTLstDb = PageResult<BanDTOs>.toPageResult(pagintation, lstDB);
                pagintation.TotalCount = lstDB.Count();

                var res = new PageResult<BanDTOs>(pagintation, PTLstDb);
                return Ok(res);
            }
            else
            {
                Pagintation pagintation = new Pagintation();
                return  Ok(services.HienThiBanTheoTrangThai(ttID, pagintation));
            }
        }

        [HttpGet]
        [Route("/api/Ban/HienThiBanTheoViTri")]
        public async Task<IActionResult> HienThiBanTheoViTri ( int pageSize, int pageNumber)
        {
            if (pageSize != 0 && pageNumber != 0)
            {
                Pagintation pagintation = new Pagintation()
                {
                    PageSize = pageSize,
                    PageNumber = pageNumber
                };

                var lstDB = await services.HienThiBanTheoViTri( pagintation);

                var PTLstDb = PageResult<BanDTOs>.toPageResult(pagintation, lstDB);
                pagintation.TotalCount = lstDB.Count();

                var res = new PageResult<BanDTOs>(pagintation, PTLstDb);
                return Ok(res);
            }
            else
            {
                Pagintation pagintation = new Pagintation();
                return Ok(services.HienThiBanTheoViTri(pagintation));
            }
        }

        [HttpGet]
        [Route("/api/Ban/HienThiBanTheoLoaiBan")]
        public async Task<IActionResult> HienThiBanTheoLoaiBan(int LB, int pageSize, int pageNumber)
        {
            if (pageSize != 0 && pageNumber != 0)
            {
                Pagintation pagintation = new Pagintation()
                {
                    PageSize = pageSize,
                    PageNumber = pageNumber
                };

                var lstDB = await services.HienThiBanTheoLoaiBan(LB, pagintation);

                var PTLstDb = PageResult<BanDTOs>.toPageResult(pagintation, lstDB);
                pagintation.TotalCount = lstDB.Count();

                var res = new PageResult<BanDTOs>(pagintation, PTLstDb);
                return Ok(res);
            }
            else
            {
                Pagintation pagintation = new Pagintation();
                return Ok(services.HienThiBanTheoLoaiBan(LB, pagintation));
            }
        }

        [HttpPost]
        [Route("/api/Ban/ThemBan")]
        [Authorize(Roles = "ADMIN,MOD")]
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
        [Route("/api/Ban/SuaBan")]
        [Authorize(Roles = "ADMIN,MOD")]
        public async Task<IActionResult> SuaBan([FromBody] Request_SuaBan request)
        {
            var result = await services.SuaBan(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("/api/Ban/XoaBan")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> XoaBan([FromBody] Request_XoaBan request)
        {
            var result = await services.XoaBan(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}