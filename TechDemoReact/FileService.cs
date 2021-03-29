using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using TechDemo.Core.Infrastructure.Services;

namespace TechDemoReact
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;
        private string _imageFolder = @"/ClientApp/public/images/";

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public void RemovePhoto(string photo)
        {
            var path = GetPath(photo);
            var file = new FileInfo(path);

            file.Delete();

        }

        public async Task<string> SaveFile(IFormFile file)
        {
            _ = file
                ?? throw new ArgumentNullException(paramName: nameof(file), message: "file should not be null");

            string uniqueName = GenerateUniqueFileName(file.FileName);

            string path = GetPath(uniqueName);

            await SaveFile(file, path);

            return uniqueName;
        }

        protected async Task SaveFile(IFormFile file, string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }

        protected string GenerateUniqueFileName(string fileName)
        {
            return $"{fileName}_{Guid.NewGuid()}{Path.GetExtension(fileName)}";
        }

        protected string GetPath(string fileName)
        {
            return _environment.ContentRootPath + ImageFolder + fileName;
        }

        protected string ImageFolder
        {
            get => _imageFolder;
            set => _imageFolder = value;
        }
    }
}
