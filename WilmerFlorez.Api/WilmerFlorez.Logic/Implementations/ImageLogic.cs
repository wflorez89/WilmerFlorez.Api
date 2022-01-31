using WilmerFlorez.Logic.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http.Headers;

namespace WilmerFlorez.Logic.Implementations
{
    public class ImageLogic : IImageLogic
    {
        private readonly ILogger<OwnerLogic> _logger;

        public ImageLogic(
            ILogger<OwnerLogic> logger)
        {
            _logger = logger;
        }

        public string Upload(IFormFile input, string webRootPath, string host)
        {
            var file = input;
            string folderName = $"Upload";
            if (string.IsNullOrEmpty(webRootPath))
            {
                webRootPath = Directory.GetCurrentDirectory();
            }

            string newPath = Path.Combine(webRootPath, folderName);
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string newname = $"{Guid.NewGuid().ToString()}_{fileName}";
                string fullPath = Path.Combine(newPath, newname);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return $"{host}/Upload//{newname}";
            }
            return string.Empty;
        }
    }
}
