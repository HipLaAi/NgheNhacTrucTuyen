using Microsoft.AspNetCore.Mvc;
using BLL;
using Model;
using DataModel;
using Microsoft.AspNetCore.Authorization;

namespace API.NgheNhacTrucTuyen.Controllers.USER
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AlbumController : Controller
    {
        private IAlbumBLL _aLbumBLL;
        public AlbumController(IAlbumBLL aLbumBLL)
        {
            _aLbumBLL = aLbumBLL;
        }

        [Route("create-album")]
        [HttpPost]
        public AlbumModel CreateAlbum([FromBody] AlbumModel model)
        {
            try
            {
                _aLbumBLL.Create(model);
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Route("delete-album")]
        [HttpDelete]
        public int Delete(int idAlbum)
        {
            _aLbumBLL.Delete(idAlbum);
            return idAlbum;
        }

        [Route("update-album")]
        [HttpPost]
        public AlbumModel UpdateItem([FromBody] AlbumModel model)
        {
            _aLbumBLL.Update(model);
            return model;
        }
        [AllowAnonymous]
        [Route("search-album")]
        [HttpPost]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var pageIndex = int.Parse(formData["pageIndex"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string tenalbum = "";
                if (formData.Keys.Contains("tenAlbum") && !string.IsNullOrEmpty(Convert.ToString(formData["tenAlbum"]))) { tenalbum = Convert.ToString(formData["tenAlbum"]); }
                long total = 0;
                var data = _aLbumBLL.Search(pageIndex, pageSize, out total, tenalbum);
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
        [Route("topnew-album")]
        [HttpPost]
        public List<AlbumModel> TopNew(int top)
        {
            return _aLbumBLL.TopNew(top);
        }

        [AllowAnonymous]
        [Route("getbyid-album")]
        [HttpGet]
        public IActionResult GetByID(int idAlbum)
        {
            var data = _aLbumBLL.GetByID(idAlbum);
            return Ok(
                new
                {
                    Data = data
                }
                );
        }

        [AllowAnonymous]
        [Route("getbyname-album")]
        [HttpGet]
        public AlbumModel GetByName(string tenAlbum)
        {
            return _aLbumBLL.GetByName(tenAlbum);
        }
    }
}