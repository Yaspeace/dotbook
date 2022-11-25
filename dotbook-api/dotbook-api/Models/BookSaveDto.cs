using dotbook_api.DataAccess.TableModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

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

        /// <summary>
        /// Идентификаторы тем книги
        /// </summary>
        public string Themes { get; set; }
    }
}
