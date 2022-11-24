using Microsoft.EntityFrameworkCore;
using dotbook_api.DataAccess.TableModels;

namespace dotbook_api.DataAccess.Context
{
    public class DotbookContext : DbContext
    {
        public DbSet<Publish> Publishes { get; set; }

        public DbSet<Theme> Themes { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<BookTheme> BookThemes { get; set; }

        public DbSet<UserFavorite> UserFavorites { get; set; }

        public DotbookContext(DbContextOptions<DotbookContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
