using dotbook_api.DataAccess.TableModels.Base;

namespace dotbook_api.DataAccess.TableModels
{
    public class BookTheme : BaseEntity
    {
        public int BookId { get; set; }

        public int ThemeId { get; set; }

        public Book Book { get; set; }

        public Theme Theme { get; set; }
    }
}
