using dotbook_api.DataAccess.TableModels;

namespace dotbook_api.Models
{
    public class BookDto : Book
    {
        public bool IsFavorite { get; set; } = false;
    }
}
