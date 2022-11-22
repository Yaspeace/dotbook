using dotbook_api.DataAccess.TableModels;
using dotbook_api.Models;
using dotbook_api.Models.Base;
using dotbook_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public IEnumerable<Book> Get([FromQuery] BaseQueryParams filter = null, [FromQuery] string search = "")
        {
            return _bookService.GetBySearch(GetUserId(), filter, search);
        }

        [Authorize]
        [HttpGet("uploads")]
        public IEnumerable<Book> GetUploads([FromQuery] BaseQueryParams filter = null, [FromQuery] string search = "")
        {
            return _bookService.GetUserUploads(GetUserId(), filter, search);
        }

        [Authorize]
        [HttpGet("favorites")]
        public IEnumerable<Book> GetFavorites([FromQuery] BaseQueryParams filter = null, [FromQuery] string search = "")
        {
            return _bookService.GetUserFavorites(GetUserId(), filter, search);
        }

        [HttpGet("bythemes")]
        public IEnumerable<Book> GetByThemes([FromQuery] IEnumerable<int> themeId, [FromQuery] BaseQueryParams filter = null, [FromQuery] string search = "")
        {
            return _bookService.GetByThemes(GetUserId(), themeId, filter, search);
        }

        [HttpPost]
        public Book Save([FromForm] BookSaveDto book)
        {
            return _bookService.Save(book, GetUserId());
        }

        private int GetUserId()
        {
            return HttpContext.User.Identity.IsAuthenticated
                ? _userService.GetIdByEmail(HttpContext.User.Identity.Name)
                : 0;
        }
    }
}
