using dotbook_api.DataAccess.TableModels;
using dotbook_api.Models;
using dotbook_api.Models.Base;
using dotbook_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
            return _bookService.GetById(id);
        }

        [HttpGet]
        public IEnumerable<Book> Get([FromQuery] BaseQueryParams filter = null, [FromQuery] string search = "")
        {
            return _bookService.GetBySearch(filter, search);
        }

        [Authorize]
        [HttpGet("uploads")]
        public IEnumerable<Book> GetUploads([FromQuery] BaseQueryParams filter = null, [FromQuery] string search = "")
        {
            var uid = _userService.GetIdByEmail(HttpContext.User.Identity.Name);
            return _bookService.GetUserUploads(uid, filter, search);
        }

        [Authorize]
        [HttpGet("favorites")]
        public IEnumerable<Book> GetFavorites([FromQuery] BaseQueryParams filter = null, [FromQuery] string search = "")
        {
            var uid = _userService.GetIdByEmail(HttpContext.User.Identity.Name);
            return _bookService.GetUserFavorites(uid, filter, search);
        }

        [HttpGet("bythemes")]
        public IEnumerable<Book> GetByThemes([FromQuery] IEnumerable<int> themeId, [FromQuery] BaseQueryParams filter = null, [FromQuery] string search = "")
        {
            return _bookService.GetByThemes(themeId, filter, search);
        }

        [HttpPost]
        public Book Save([FromForm] BookSaveDto book)
        {
            var uid = 1; //_userService.GetIdByEmail(_httpContext.User.Identity.Name);
            return _bookService.Save(book, uid);
        }
    }
}
