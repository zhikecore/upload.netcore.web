using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace upload.netcore.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private IHostingEnvironment _environment;
        public MediaController(IHostingEnvironment env)
        {
            _environment = env;
        }

        /// <summary>
        /// 新增（上传）多媒体文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json", "multipart/form-data")]
        public async Task<IActionResult> PostMedia([FromForm]IFormFile file)
        {
            var formFile = Request.Form.Files.FirstOrDefault();

            if (formFile == null)
            {
                throw new Exception("文件不能为空");
            }

            //文件上传
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return Ok(new { success = true });
        }
    }
}