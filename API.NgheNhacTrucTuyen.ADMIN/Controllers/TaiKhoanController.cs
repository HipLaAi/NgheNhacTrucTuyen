using Microsoft.AspNetCore.Mvc;
using BLL;
using Model;
using DataModel;
using Microsoft.AspNetCore.Authorization;

namespace API.NgheNhacTrucTuyen.Controllers.ADMIN
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaiKhoanController : Controller
    {
        private ITaiKhoanBLL _taiKhoanBLL;
        public TaiKhoanController(ITaiKhoanBLL taiKhoanBLL)
        {
            _taiKhoanBLL = taiKhoanBLL;
        }

        [Route("create-account")]
        [HttpPost]
        public IActionResult CreateTaiKhoans([FromBody] TaiKhoanAndChietTietTaiKhoanModel combinedModel)
        {
            try
            {
                _taiKhoanBLL.Create(combinedModel.taiKhoan, combinedModel.chiTietTaiKhoanModel);
                return Ok(
                    new
                    {
                        LoaiTaiKhoan = combinedModel.taiKhoan.IDLoaiTaiKhoan,
                        TenDangNhap = combinedModel.taiKhoan.TenDangNhap,
                        MatKhau = combinedModel.taiKhoan.MatKhau,
                        Email = combinedModel.taiKhoan.Email,
                        HoTen = combinedModel.chiTietTaiKhoanModel.HoTen,
                        DiaChi = combinedModel.chiTietTaiKhoanModel.DiaChi,
                        GioiTinh = combinedModel.chiTietTaiKhoanModel.GioiTinh,
                        NgaySinh = combinedModel.chiTietTaiKhoanModel.NgaySinh,
                        SDT = combinedModel.chiTietTaiKhoanModel.SDT,
                        AnhDaiDien = combinedModel.chiTietTaiKhoanModel.AnhDaiDien,
                    }
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
        public IActionResult UpdateItem([FromBody] TaiKhoanAndChietTietTaiKhoanModel combinedModel)
        {
            try
            {
                _taiKhoanBLL.Update(combinedModel.taiKhoan, combinedModel.chiTietTaiKhoanModel);
                return Ok(
                    new
                    {
                        TenDangNhap = combinedModel.taiKhoan.TenDangNhap,
                        MatKhau = combinedModel.taiKhoan.MatKhau,
                        Email = combinedModel.taiKhoan.Email,
                        HoTen = combinedModel.chiTietTaiKhoanModel.HoTen,
                        DiaChi = combinedModel.chiTietTaiKhoanModel.DiaChi,
                        GioiTinh = combinedModel.chiTietTaiKhoanModel.GioiTinh,
                        NgaySinh = combinedModel.chiTietTaiKhoanModel.NgaySinh,
                        SDT = combinedModel.chiTietTaiKhoanModel.SDT,
                        AnhDaiDien = combinedModel.chiTietTaiKhoanModel.AnhDaiDien,
                    }
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [AllowAnonymous]
        [Route("login-account")]
        [HttpPost]
        public IActionResult DangNhap([FromBody] AuthenticateModel model)
        {
            var user = _taiKhoanBLL.DangNhap(model.Username, model.Password);
            if (user == null)
                return BadRequest(new { message = "Tài khoản hoặc mật khẩu không đúng!" });
            return Ok(new { tendangnhap = user.TenDangNhap, email = user.Email, token = user.Token });
        }

        [Route("search-account")]
        [HttpPost]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var pageIndex = int.Parse(formData["pageIndex"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string hoten = "";
                if (formData.Keys.Contains("hoTen") && !string.IsNullOrEmpty(Convert.ToString(formData["hoTen"]))) { hoten = Convert.ToString(formData["hoTen"]); }
                string diachi = "";
                if (formData.Keys.Contains("diaChi") && !string.IsNullOrEmpty(Convert.ToString(formData["diaChi"]))) { diachi = Convert.ToString(formData["diaChi"]); }
                string gioitinh = "";
                if (formData.Keys.Contains("gioiTinh") && !string.IsNullOrEmpty(Convert.ToString(formData["gioiTinh"]))) { gioitinh = Convert.ToString(formData["gioiTinh"]); }
                long total = 0;
                var data = _taiKhoanBLL.Search(pageIndex, pageSize, out total, hoten, diachi, gioitinh);
                return Ok(
                    new
                    {
                        TotalItems = total,
                        Data = data,
                        Page = pageIndex,
                        PageSize = pageSize
                    }
                    );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Route("getbyid-account")]
        [HttpGet]
        public TaiKhoanModel GetByID (int idTaiKhoan)
        {
            return _taiKhoanBLL.GetByID(idTaiKhoan);
        }
    }
}