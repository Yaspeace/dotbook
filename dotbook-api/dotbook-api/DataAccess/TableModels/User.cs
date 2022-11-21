using dotbook_api.DataAccess.TableModels.Base;

namespace dotbook_api.DataAccess.TableModels
{
    public class User : BaseNameEntity
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
