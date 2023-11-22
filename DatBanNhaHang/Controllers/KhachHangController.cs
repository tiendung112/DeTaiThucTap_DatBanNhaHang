using DatBanNhaHang.Entities.NhaHang;
using DatBanNhaHang.Pagination;
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
        [Authorize(Roles = "ADMIN,MOD")]
        public async Task<IActionResult> HienThiKhachHang(int pageSize, int pageNumber)
        {
            if (pageSize != 0&& pageNumber != 0)
            {
                Pagintation pagintation = new Pagintation()
                {
                    PageSize = pageSize,
                    PageNumber = pageNumber
                };

                var lstDB = await services.HienThiKhachHang(pagintation);

                var PTLstDb = PageResult<KhachHangDTOs>.toPageResult(pagintation, lstDB);
                pagintation.TotalCount = lstDB.Count();

                var res = new PageResult<KhachHangDTOs>(pagintation, PTLstDb);
                return Ok(res);
            }
            else
            {
                Pagintation pagintation = new Pagintation();
                return Ok(services.HienThiKhachHang(pagintation));
            }
        }
        [HttpGet]
        [Route("/api/KhachHang/TimKiemKhachHang")]
        [Authorize(Roles = "ADMIN,MOD")]
        public async Task<IActionResult> TimKiemKhachHang([FromBody] Request_TimKiemKhachHang request,int pageSize, int pageNumber)
        {
            if (pageSize != 0&& pageNumber != 0)
            {
                Pagintation pagintation = new Pagintation()
                {
                    PageSize = pageSize,
                    PageNumber = pageNumber
                };

                var lstDB = await services.TimKiemKhachHang(request,pagintation);
                if (lstDB == null)
                {
                    return BadRequest("Không tìm thấy khách hàng");
                }
                var PTLstDb = PageResult<KhachHangDTOs>.toPageResult(pagintation, lstDB);
                pagintation.TotalCount = lstDB.Count();

                var res = new PageResult<KhachHangDTOs>(pagintation, PTLstDb);
                return Ok(res);
            }
            else
            {
                Pagintation pagintation = new Pagintation();
                var lstDB = await services.TimKiemKhachHang(request, pagintation);
                if (lstDB == null)
                {
                    return BadRequest("Không tìm thấy khách hàng");
                }

                return Ok(lstDB);
            }
        }

        [HttpPost]
        [Route("/api/KhachHang/ThemKhachHang")]
        [Authorize(Roles ="ADMIN,MOD")]
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
        [Route("/api/KhachHang/SuaKhachHang")]
        [Authorize(Roles = "ADMIN,MOD")]
        public async Task<IActionResult> SuaKhachHang([FromBody] Request_SuaKhachHang request)
        {
            var result = await services.SuaKhachHang(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete]
        [Route("/api/KhachHang/XoaKhachHang")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> XoaKhachHang([FromBody] Request_XoaKhachHang request)
        {
            var result = await services.XoaKhachHang(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
