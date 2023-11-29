using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.NgheNhacTrucTuyen.Controllers.ADMIN
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class NgheSiController : Controller
    {
        private INgheSiBLL _nghesiBLL;
        public NgheSiController(INgheSiBLL nghesiBLL)
        {
            _nghesiBLL = nghesiBLL;
        }
        //[Route("create-nghesi")]
        //[HttpPost]
        //public NgheSiModel CreateNhac([FromBody] NgheSiModel model)
        //{
        //    try
        //    {
        //        _nghesiBLL.Create(model);
        //        return model;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        //------------------------------------
        [AllowAnonymous]
        [Route("create-nghesi")]
        [HttpPost]
        public IActionResult CreateNgheSi([FromBody] NgheSiModel model)
        {
            var music = _nghesiBLL.Create(model);
            if (music != null)
                return Ok(new { message = "Nghệ sĩ đã tồn tại" });
            return Ok(new { message = "Thêm thành công" });
        }
        //-----------------------------------

        [Route("delete-nghesi")]
        [HttpDelete]
        public int Delete(int idTheLoai)
        {
            _nghesiBLL.Delete(idTheLoai);
            return idTheLoai;
        }

        [Route("update-nghesi")]
        [HttpPost]
        public NgheSiModel UpdateItem([FromBody] NgheSiModel model)
        {
            _nghesiBLL.Update(model);
            return model;
        }
        //[AllowAnonymous]
        [Route("search-nghesi")]
        [HttpPost]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var pageIndex = int.Parse(formData["pageIndex"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string tenNgheSi = "";
                if (formData.Keys.Contains("tenNgheSi") && !string.IsNullOrEmpty(Convert.ToString(formData["tenNgheSi"]))) { tenNgheSi = Convert.ToString(formData["tenNgheSi"]); }
                long total = 0;
                var data = _nghesiBLL.Search(pageIndex, pageSize, out total, tenNgheSi);
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
        [AllowAnonymous]
        [Route("getbyid-nghesi")]
        [HttpGet]
        public IActionResult GetByID(int idNgheSi)
        {
            var data = _nghesiBLL.GetByID(idNgheSi);
            return Ok(
                new
                {
                    Data = data
                }
                );
        }
        [AllowAnonymous]
        [Route("getbyname-nghesi")]
        [HttpGet]
        public NgheSiModel GetByName(string tenNgheSi)
        {
            return _nghesiBLL.GetByName(tenNgheSi);
        }

        [AllowAnonymous]
        [Route("getall-nghesi")]
        [HttpGet]
        public List<NgheSiModel> GetAll()
        {
            return _nghesiBLL.GetAll();
        }
    }
}
