using System.IO;
using _0_Framework.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ServiceHost
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public string Upload(IFormFile file, string path)
        {
            if (file == null)
                return "";

            var directoryPath = $"{_webHostEnvironment.WebRootPath}//UploadedPictures//{path}";

            if (Directory.Exists(directoryPath) is not true)
                Directory.CreateDirectory(directoryPath);

            var filePath = $"{directoryPath}//{file.FileName}";

            using var output = File.Create(filePath);
            file.CopyTo(output);

            return $"{path}/{file.FileName}";
        }
    }
}
