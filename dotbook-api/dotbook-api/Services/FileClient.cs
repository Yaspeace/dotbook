using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace dotbook_api.Services
{
    public class FileClient
    {
        private readonly HttpClient _client;
        private readonly string _apisaveUrl;

        public FileClient(IConfiguration configuration)
        {
            _apisaveUrl = configuration.GetSection("CloudUrls").GetValue<string>("ApiSave");
            _client = new HttpClient();
        }

        public async Task<string> SaveImageAsync(IFormFile image, int id)
        {
            return await SaveFileAsync(image, id, true, "png");
        }

        public async Task<string> SavePdfAsync(IFormFile pdf, int id)
        {
            return await SaveFileAsync(pdf, id, false, "pdf");
        }

        private async Task<string> SaveFileAsync(IFormFile file, int id, bool isImage, string postfix)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(file.OpenReadStream()), "file", file.Name);
            content.Add(new StringContent(id.ToString()), "id");
            content.Add(new StringContent(postfix), "postfix");
            content.Add(new StringContent(isImage ? "1" : "0"), "isImage");
            var result = new HttpResponseMessage();
            try
            {
                result = await _client.PostAsync(_apisaveUrl, content);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            var message = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode) return message;
            throw new System.Exception($"Ошибка сохранения файла: {message}");
        }
    }
}
