using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageProcessor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        public UploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost("UploadFile")]
        public async Task<string> UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                //string fName = file.FileName;
                string fName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string path = Path.Combine(_environment.ContentRootPath, "Images", fName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return path;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost()]
        public async Task<IActionResult> GetImage(string imageName)
        {
            Byte[] b;
            b = await System.IO.File.ReadAllBytesAsync(Path.Combine(_environment.ContentRootPath, "Images", $"{imageName}"));
            return File(b, "image/jpeg");
        }
    }
}
