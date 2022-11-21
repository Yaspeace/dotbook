using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotbook_api.DataAccess.Context;
using dotbook_api.DataAccess.TableModels;
using dotbook_api.Extensions;
using dotbook_api.Models;
using dotbook_api.Models.Base;
using dotbook_api.Services.Base;

namespace dotbook_api.Services
{
    public class BookService : BaseCrudService<Book>
    {
        private readonly BaseCrudService<SavedFile> _fileService;
        private readonly FileClient _fileClient;
        public BookService(
            DotbookContext context,
            FileClient fileClient,
            BaseCrudService<SavedFile> fileService) : base(context) 
        {
            _fileClient = fileClient;
            _fileService = fileService;
        }

        public IEnumerable<Book> GetBySearch(BaseQueryParams filter = null, string search = "")
        {
            return this.Get().Filter(filter).Where(x => x.Name.ToLower().Contains(search.ToLower())).ToArray();
        }

        public IEnumerable<Book> GetUserUploads(int userId, BaseQueryParams filter = null, string search = "")
        {
            return this.Get().Filter(filter).Where(x => x.UploadUserId == userId && x.Name.ToLower().Contains(search.ToLower())).ToArray();
        }

        public IEnumerable<Book> GetUserFavorites(int userId, BaseQueryParams filter = null, string search = "")
        {
            var bookIds = _context.UserFavorites
                .Where(x => x.UserId == userId)
                .Filter(filter)
                .Select(x => x.BookId).ToArray();
            return _context.Books.Where(x => bookIds.Contains(x.Id) && x.Name.ToLower().Contains(search.ToLower())).ToArray();
        }

        public IEnumerable<Book> GetByThemes(IEnumerable<int> themesIds, BaseQueryParams filter = null, string search = "")
        {
            var bookIds = _context.BookThemes
                .Where(x => themesIds.Contains(x.ThemeId))
                .Filter(filter)
                .Select(x => x.BookId).ToArray();
            return _context.Books.Where(x => bookIds.Contains(x.Id) && x.Name.ToLower().Contains(search.ToLower())).ToArray();
        }

        public Book Save(BookSaveDto book, int userId)
        {
            Book bookToSave = book;
            var img = new SavedFile();
            var pdf = new SavedFile();

            img = _fileService.Add(img);
            pdf = _fileService.Add(pdf);

            _context.SaveChanges();

            img.Name = _fileClient.SaveImage(book.FileImg, img.Id);
            pdf.Name = _fileClient.SavePdf(book.FilePdf, pdf.Id);

            bookToSave.ImageId = img.Id;
            bookToSave.PdfId = pdf.Id;
            bookToSave.UploadUserId = userId;

            var res = this.Add(bookToSave);
            _context.SaveChanges();
            return res;
        }
    }
}
