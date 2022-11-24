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

        public int GetIdByEmail(string name)
        {
            return _context.Users.Where(x => x.Name == name).Select(x => x.Id).FirstOrDefault();
        }
    }
}
