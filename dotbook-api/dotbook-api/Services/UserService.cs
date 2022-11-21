using dotbook_api.DataAccess.Context;
using System.Linq;

namespace dotbook_api.Services
{
    public class UserService
    {
        private readonly DotbookContext _context;

        public UserService(DotbookContext context)
        {
            _context = context;
        }

        public int GetIdByEmail(string email)
        {
            return _context.Users.Where(x => x.Email == email).Select(x => x.Id).FirstOrDefault();
        }
    }
}
