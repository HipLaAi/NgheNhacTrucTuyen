using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.NgheNhacTrucTuyen.Controllers.ADMIN
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolController : Controller
    {
        private IToolBLL _toolBLL;
        public ToolController(IToolBLL toolBLL)
        {
            _toolBLL = toolBLL;
        }

        [Route("upload_IMG-tool")]
        [HttpPost]
        public async Task<IActionResult> UploadIMG(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    string filePath = $"IMG/{file.FileName.Replace("-", "_").Replace("%", "")}";
                    var fullPath = _toolBLL.CreatePathFile(filePath);
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return Ok(new { fullPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Không thể upload tệp");
            }
        }

        [Route("upload_AUDIO-tool")]
        [HttpPost]
        public async Task<IActionResult> UploadAudio(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    string filePath = $"AUDIO/{file.FileName.Replace("-", "_").Replace("%", "")}";
                    var fullPath = _toolBLL.CreatePathFile(filePath);
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return Ok(new { fullPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Không thể upload tệp");
            }
        }

        [Route("upload_TEXT-tool")]
        [HttpPost]
        public async Task<IActionResult> UploadTXT(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    string filePath = $"TEXT/{file.FileName.Replace("-", "_").Replace("%", "")}";
                    var fullPath = _toolBLL.CreatePathFile(filePath);
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return Ok(new { fullPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Không thể upload tệp");
            }
        }
    }
}
