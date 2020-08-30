using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace upload.netcore.web.Controllers
{
    public class TestController : Controller
    {
        private IHostingEnvironment _environment;
        public TestController(IHostingEnvironment env)
        {
            _environment = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///示例 jquery ajax 调用webapi 上传文件
        /// </summary>
        /// <returns></returns>
        public IActionResult UploadByAjax()
        {
            return View();
        }

        /// <summary>
        /// 示例 .net core mvc 调用 mvc 上传文件
        /// </summary>
        /// <returns></returns>
        public IActionResult UploadByRazor()
        {
            return View();
        }

        [HttpPost]
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