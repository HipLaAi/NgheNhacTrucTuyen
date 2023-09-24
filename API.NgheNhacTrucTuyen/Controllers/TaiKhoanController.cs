using Microsoft.AspNetCore.Mvc;
using BLL;
using Model;

namespace API.NgheNhacTrucTuyen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : Controller
    {
        private ITaiKhoanBLL _taiKhoanBLL;
        public TaiKhoanController(ITaiKhoanBLL taiKhoanBLL)
        {
            _taiKhoanBLL = taiKhoanBLL;
        }

        [Route("get-taikhoan-by-tendangnhap/{tenDangnhap}")]
        [HttpGet]
        public TaiKhoanModel GetTaiKhoans(string tenDangnhap)
        {
            return _taiKhoanBLL.GetTaiKhoan(tenDangnhap);
        }
    }
}
