using Microsoft.AspNetCore.Mvc;
using BLL;
using Model;
using DataModel;
using Microsoft.AspNetCore.Authorization;

namespace API.NgheNhacTrucTuyen.Controllers.USER
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


        //[Route("get-taikhoan-by-tendangnhap/{tenDangnhap}")]
        //[HttpGet]
        //public TaiKhoanModel GetTaiKhoans(string tenDangnhap)
        //{
        //    return _taiKhoanBLL.GetTaiKhoan(tenDangnhap);
        //}
        [Route("create-account")]
        [HttpPost]
        public TaiKhoanModel CreateTaiKhoans([FromBody] TaiKhoanModel model)
        {
            _taiKhoanBLL.Create(model);
            return model;
        }

        [Route("delete-account")]
        [HttpDelete]
        public string Delete(string tenDangNhap)
        {
            _taiKhoanBLL.Delete(tenDangNhap);
            return tenDangNhap;
        }

        [Route("update-account")]
        [HttpPost]
        public TaiKhoanModel UpdateItem([FromBody] TaiKhoanModel model)
        {
            _taiKhoanBLL.Update(model);
            return model;
        }

        [AllowAnonymous]
        [HttpPost("login-account")]
        public IActionResult DangNhap([FromBody] AuthenticateModel model)
        {
            var user = _taiKhoanBLL.DangNhap(model.Username, model.Password);
            if (user == null)
                return BadRequest(new { message = "Tài khoản hoặc mật khẩu không đúng!" });
            return Ok(new { tendangnhap = user.TenDangNhap, email = user.Email, token = user.Token });
        }
    }
}