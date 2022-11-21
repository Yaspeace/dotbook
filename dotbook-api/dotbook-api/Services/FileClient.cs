using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace dotbook_api.Services
{
    public class FileClient
    {
        private readonly string _imgDir;
        private readonly string _pdfDir;

        public FileClient(IConfiguration configuration)
        {
            _imgDir = configuration.GetSection("CloudUrls").GetValue<string>("ImageDir");
            _pdfDir = configuration.GetSection("CloudUrls").GetValue<string>("PdfDir");
        }

        public string SaveImage(IFormFile image, int id)
        {
            return SaveFile(image, id, _imgDir, "image", "png");
        }

        public string SavePdf(IFormFile pdf, int id)
        {
            return SaveFile(pdf, id, _pdfDir, "pdf", "pdf");
        }

        public async Task<string> SaveImageAsync(IFormFile image, int id)
        {
            return await SaveFileAsync(image, id, _imgDir, "image", "png");
        }

        public async Task<string> SavePdfAsync(IFormFile pdf, int id)
        {
            return await SaveFileAsync(pdf, id, _pdfDir, "pdf", "pdf");
        }

        private string SaveFile(IFormFile file, int id, string dir, string prefix, string suffix)
        {
            var dirpath = "/" + dir;
            if (!Directory.Exists(dirpath)) Directory.CreateDirectory(dirpath);
            var path = dirpath + "/" + prefix + id.ToString() + "." + suffix;
            if (File.Exists(path)) File.Delete(path);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return path;
        }

        private async Task<string> SaveFileAsync(IFormFile file, int id, string dir, string prefix, string suffix)
        {
            var dirpath = "/" + dir;
            if (!Directory.Exists(dirpath)) Directory.CreateDirectory(dirpath);
            var path = dirpath + "/" + prefix + id.ToString() + "." + suffix;
            if (File.Exists(path)) File.Delete(path);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return path;
        }
    }
}
