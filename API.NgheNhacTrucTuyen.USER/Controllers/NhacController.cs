using Microsoft.AspNetCore.Mvc;
using BLL;
using Model;
using DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace API.NgheNhacTrucTuyen.Controllers.USER
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NhacController : Controller
    {
        private INhacBLL _nhacBLL;
        public NhacController(INhacBLL nhacBLL)
        {
            _nhacBLL = nhacBLL;
        }

        [Route("create-nhac")]
        [HttpPost]
        public IActionResult CreateNhac([FromBody] NhacModel model)
        {
            var music = _nhacBLL.Create(model);
            if (music != null)
                return Ok(new { message = "Nhạc đã tồn tài" });
            return Ok(new { message = "Thêm thành công" });
        }

        [Route("delete-nhac")]
        [HttpDelete]
        public int Delete(int idNhac)
        {
            _nhacBLL.Delete(idNhac);
            return idNhac;
        }

        [Route("update-nhac")]
        [HttpPost]
        public NhacModel UpdateItem([FromBody] NhacModel model)
        {
            _nhacBLL.Update(model);
            return model;
        }

        [Route("search-nhac")]
        [HttpPost]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var pageIndex = int.Parse(formData["pageIndex"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string tennhac = "";
                if (formData.Keys.Contains("tenNhac") && !string.IsNullOrEmpty(Convert.ToString(formData["tenNhac"]))) { tennhac = Convert.ToString(formData["tenNhac"]); }
                var idtheloai = 0;
                if (formData.Keys.Contains("idTheLoai") && !string.IsNullOrEmpty(Convert.ToString(formData["idTheLoai"]))) { idtheloai = int.Parse(formData["idTheLoai"].ToString()); }
                var idnghesi = 0;
                if (formData.Keys.Contains("idNgheSi") && !string.IsNullOrEmpty(Convert.ToString(formData["idNgheSi"]))) { idnghesi = int.Parse(formData["idNgheSi"].ToString()); }
                long total = 0;
                var data = _nhacBLL.Search(pageIndex, pageSize, out total, tennhac, idtheloai, idnghesi);
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

        [Route("getbyid-nhac")]
        [HttpGet]
        public IActionResult GetByID(int idNhac)
        {
            var data = _nhacBLL.GetByID(idNhac);
            return Ok(
                new
                {
                    Data = data
                }
                ) ;
        }

        [AllowAnonymous]
        [Route("toplove-nhac")]
        [HttpGet]
        public IActionResult TopLove(int top)
        {
            var data = _nhacBLL.GetByID(top);
            return Ok(
                new
                {
                    Data = data
                }
                );
        }

        [AllowAnonymous]
        [Route("getnhacbyidnghesi-nhac")]
        [HttpGet]
        public IActionResult GetNhacByIDNgheSi(int idNgheSi)
        {
            var data = _nhacBLL.GetNhacByIDNgheSi(idNgheSi);
            return Ok(
                new
                {
                    Data = data
                }
                );
        }

        [AllowAnonymous]
        [Route("updateview-nhac")]
        [HttpPost]
        public bool UpdateView(int idNhac)
        {
            return _nhacBLL.UpdateView(idNhac);
        }

    }
}
