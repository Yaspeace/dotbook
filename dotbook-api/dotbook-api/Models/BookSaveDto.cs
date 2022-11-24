using dotbook_api.DataAccess.TableModels;
using Microsoft.AspNetCore.Http;

namespace dotbook_api.Models
{
    public class BookSaveDto : Book
    {
        /// <summary>
        /// Обложка
        /// </summary>
        public IFormFile FileImg { get; set; }

        /// <summary>
        /// Текст книги в формате PDF
        /// </summary>
        public IFormFile FilePdf { get; set; }
    }
}
