using dotbook_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotbook_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : Controller
    {
        private readonly FavoritesService _service;
        private readonly UserService _uSrv;

        public FavoritesController(FavoritesService service, UserService uSrv)
        {
            _service = service;
            _uSrv = uSrv;
        }

        [HttpPost]
        public bool AddToFavorites(int bookId)
        {
            int uid = _uSrv.GetIdByEmail(HttpContext.User.Identity.Name);
            return _service.AddFavorite(uid, bookId);
        }

        [HttpPost("delete")]
        public void DeleteFavorite(int bookId)
        {
            int uid = _uSrv.GetIdByEmail(HttpContext.User.Identity.Name);
            _service.RemoveFavorite(uid, bookId);
        }
    }
}
