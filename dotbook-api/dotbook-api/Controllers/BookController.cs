using dotbook_api.DataAccess.TableModels;
using dotbook_api.Models;
using dotbook_api.Models.Base;
using dotbook_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotbook_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly BookService _bookService;
        private readonly UserService _userService;

        public BookController(BookService bookService,
            UserService userService)
        {
            _bookService = bookService;
            _userService = userService;
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            return _bookService.GetById(id, GetUserId());
        }

        [HttpGet]
        public IEnumerable<BookDto> Get([FromQuery] BaseQueryParams filter = null, [FromQuery] string search = "")
        {
            return _bookService.GetBySearch(GetUserId(), filter, search);
        }

        [HttpGet("count")]
        public int GetCount([FromQuery] string search = "")
        {
            return _bookService.Get(search: search).Count();
        }

        [Authorize]
        [HttpGet("uploads")]
        public IEnumerable<BookDto> GetUploads([FromQuery] BaseQueryParams filter = null, [FromQuery] string search = "")
        {
            return _bookService.GetUserUploads(GetUserId(), filter, search);
        }

        [Authorize]
        [HttpGet("uploads/count")]
        public int UploadsCount([FromQuery] string search = "")
        {
            return _bookService.GetUserUploads(GetUserId(), null, search).Count();
        }

        [Authorize]
        [HttpGet("favorites")]
        public IEnumerable<BookDto> GetFavorites([FromQuery] BaseQueryParams filter = null, [FromQuery] string search = "")
        {
            return _bookService.GetUserFavorites(GetUserId(), filter, search);
        }

        [Authorize]
        [HttpGet("favorites/count")]
        public int FavoritesCount([FromQuery] string search = "")
        {
            return _bookService.GetUserFavorites(GetUserId(), null, search).Count();
        }

        [HttpGet("bythemes")]
        public IEnumerable<BookDto> GetByThemes([FromQuery] IEnumerable<int> themeId, [FromQuery] BaseQueryParams filter = null, [FromQuery] string search = "")
        {
            return _bookService.GetByThemes(GetUserId(), themeId, filter, search);
        }

        [HttpGet("bythemes/count")]
        public int ByThemesCount([FromQuery] IEnumerable<int> themeId, [FromQuery] string search = "")
        {
            return _bookService.GetByThemes(0, themeId, null, search).Count();
        }

        [HttpPost]
        public async Task<ResponceData<Book>> Save([FromForm] BookSaveDto book)
        {
            try
            {
                var data = await _bookService.Save(book, GetUserId());
                return ResponceData<Book>.Success(data);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return ResponceData<Book>.Error(ex);
            }
        }

        private int GetUserId()
        {
            return HttpContext.User.Identity.IsAuthenticated
                ? _userService.GetIdByEmail(HttpContext.User.Identity.Name)
                : 0;
        }
    }
}
