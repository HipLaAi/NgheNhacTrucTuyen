using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.NgheNhacTrucTuyen.Controllers.ADMIN
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheLoaiController : Controller
    {
        private ITheLoaiBLL _theloaiBLL;
        public TheLoaiController(ITheLoaiBLL theloaiBLL)
        {
            _theloaiBLL = theloaiBLL;
        }

        [Route("search-theloai")]
        [HttpPost]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var pageIndex = int.Parse(formData["pageIndex"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string tentheloai = "";
                if (formData.Keys.Contains("tenTheLoai") && !string.IsNullOrEmpty(Convert.ToString(formData["tenTheLoai"]))) { tentheloai = Convert.ToString(formData["tenTheLoai"]); }
                long total = 0;
                var data = _theloaiBLL.Search(pageIndex, pageSize, out total, tentheloai);
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
        [Route("getbyname-theloai")]
        [HttpGet]
        public TheLoaiModel GetByName(string tenTheLoai)
        {
            return _theloaiBLL.GetByName(tenTheLoai);
        }

        [Route("getbyid-theloai")]
        [HttpGet]
        public List<TheLoaiModel> GetByID(int idTheLoai)
        {
            return _theloaiBLL.GetByID(idTheLoai);
        }

        [Route("getall-theloai")]
        [HttpGet]
        [AllowAnonymous]
        public List<TheLoaiModel> GetAll()
        {
            return _theloaiBLL.GetAll();
        }

        [Route("tophot-theloai")]
        [HttpGet]
        public IActionResult TopHot(int top)
        {
            var data = _theloaiBLL.TopHot(top);
            return Ok(
                new
                {
                    Data = data
                }
                );
        }
    }
}
