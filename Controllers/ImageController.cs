using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageProcessor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private string FilePathError = "First param has to be FilePath";

        [HttpPost]
        public async Task<string> Post([FromBody] IDictionary<string, string> userData)
        {
            if(!userData.First().Key.Equals("FilePath"))
            {
                return this.FilePathError;
            }
            
            var filePath =  ImageTransformer.ProcessImage(userData);
            byte[] imageArray = System.IO.File.ReadAllBytes(filePath);
            return Convert.ToBase64String(imageArray);
        }
    }
}
