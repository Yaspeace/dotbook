using dotbook_api.DataAccess.Context;
using dotbook_api.DataAccess.TableModels;
using dotbook_api.Services.Base;
using System.Linq;

namespace dotbook_api.Services
{
    public class FavoritesService : BaseCrudService<UserFavorite>
    {
        public FavoritesService(DotbookContext ctx) : base(ctx) { }

        public bool AddFavorite(int userId, int bookId)
        {
            if (userId <= 0) return false;
            var toAdd = new UserFavorite()
            {
                UserId = userId,
                BookId = bookId
            };
            if (_context.UserFavorites.Any(x => x.UserId == userId && x.BookId == bookId)) return true;
            _context.UserFavorites.Add(toAdd);
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void RemoveFavorite(int userId, int bookId)
        {
            var toDelete = _context.UserFavorites.FirstOrDefault(x => x.UserId == userId && x.BookId == bookId);
            if (toDelete != null) _context.UserFavorites.Remove(toDelete);
            _context.SaveChanges();
        }
    }
}
