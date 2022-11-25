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
using Microsoft.EntityFrameworkCore;

namespace dotbook_api.Services
{
    public class BookService
    {
        private readonly DotbookContext _context;
        private readonly BaseCrudService<SavedFile> _fileService;
        private readonly FileClient _fileClient;
        public BookService(
            DotbookContext context,
            FileClient fileClient,
            BaseCrudService<SavedFile> fileService)
        {
            _fileClient = fileClient;
            _fileService = fileService;
            _context = context;
        }

        public IQueryable<Book> Get(BaseQueryParams filter = null, string search = "")
        {
            return _context.Books
                .Include(x => x.Image)
                .Include(x => x.Pdf)
                .Include(x => x.UploadUser)
                .Include(x => x.Publish)
                .Filter(filter)
                .Search(search);
        }

        public BookDto GetById(int id, int userId)
        {
            var dbBook = this.Get().FirstOrDefault(x => x.Id == id) ?? throw new Exception($"Книга с id: {id} не найдена");
            var book = MapToDto(dbBook);
            if (userId > 0)
            {
                book.IsFavorite = _context.UserFavorites.Any(x => x.UserId == userId && x.BookId == book.Id);
            }
            return book;
        }

        public IEnumerable<BookDto> GetBySearch(int userId, BaseQueryParams filter = null, string search = "")
        {
            var books = this.Get(filter, search).ToArray().Select(x => MapToDto(x)).ToArray();
            SetFavorites(books, userId);
            return books;
        }

        public IEnumerable<BookDto> GetUserUploads(int userId, BaseQueryParams filter = null, string search = "")
        {
            var books = this.Get(filter, search).Where(x => x.UploadUserId == userId).ToArray().Select(x => MapToDto(x)).ToArray();
            SetFavorites(books, userId);
            return books;
        }

        public IEnumerable<BookDto> GetUserFavorites(int userId, BaseQueryParams filter = null, string search = "")
        {
            var bookIds = _context.UserFavorites
                .Where(x => x.UserId == userId)
                .Select(x => x.BookId).ToArray();
            var books = this.Get(filter, search).Where(x => bookIds.Contains(x.Id)).ToArray().Select(x => MapToDto(x)).ToArray();
            foreach (var book in books) book.IsFavorite = true;
            return books;
        }

        public IEnumerable<BookDto> GetByThemes(int userId, IEnumerable<int> themesIds, BaseQueryParams filter = null, string search = "")
        {
            var bookIds = _context.BookThemes
                .Where(x => themesIds.Contains(x.ThemeId))
                .Select(x => x.BookId).ToArray();
            var books = this.Get(filter, search).Where(x => bookIds.Contains(x.Id)).ToArray().Select(x => MapToDto(x)).ToArray();
            SetFavorites(books, userId);
            return books;
        }

        public async Task<BookDto> Save(BookSaveDto book, int userId)
        {
            Book bookToSave = book;
            var img = new SavedFile();
            var pdf = new SavedFile();

            img = _fileService.Add(img);
            pdf = _fileService.Add(pdf);

            _context.SaveChanges();

            try
            {
                img.Name = await _fileClient.SaveImageAsync(book.FileImg, img.Id);
                pdf.Name = await _fileClient.SavePdfAsync(book.FilePdf, pdf.Id);
            }
            catch (Exception ex)
            {
                _context.Remove(img);
                _context.Remove(pdf);
                _context.SaveChanges();
                throw new Exception(ex.Message);
            }

            bookToSave.ImageId = img.Id;
            bookToSave.PdfId = pdf.Id;
            bookToSave.UploadUserId = userId;

            var res = _context.Books.Add(bookToSave);
            _context.SaveChanges();

            var themes = book.Themes.Split(',');
            foreach (var themeId in themes)
            {
                _context.BookThemes.Add(new BookTheme()
                {
                    BookId = res.Entity.Id,
                    ThemeId = int.Parse(themeId)
                });
            }
            _context.SaveChanges();

            return GetById(res.Entity.Id, userId);
        }

        private BookDto MapToDto(Book entity)
        {
            return new BookDto()
            {
                Id = entity.Id,
                Author = entity.Author,
                Image = entity.Image,
                Name = entity.Name,
                Pdf = entity.Pdf,
                Publish = entity.Publish,
                PublishYear = entity.PublishYear,
                UploadUser = entity.UploadUser,
                IsFavorite = false
            };

        }

        /// <summary>
        /// Установить у книг флаг, являются ли они избранными для данного юзера
        /// </summary>
        /// <param name="books">Список книг</param>
        /// <param name="userId">Id пользователя</param>
        private void SetFavorites(IEnumerable<BookDto> books, int userId)
        {
            if (userId <= 0) return;

            var favoritesIds = _context.UserFavorites.Where(x => x.UserId == userId).Select(x => x.BookId).ToArray();
            foreach (var book in books) book.IsFavorite = favoritesIds.Contains(book.Id);
        }
    }
}
