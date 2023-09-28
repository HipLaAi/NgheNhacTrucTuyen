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
        [Route("create-taikhoan")]
        [HttpPost]
        public TaiKhoanModel CreateTaiKhoans([FromBody] TaiKhoanModel model)
        {
            _taiKhoanBLL.Create(model);
            return model;
        }
        [Route("delete-taikhoan")]
        [HttpDelete]
        public bool Delete(string tenDangNhap) 
        {
            return _taiKhoanBLL.Delete(tenDangNhap);
        }
    }
}
