using dotbook_api.DataAccess.TableModels.Base;

namespace dotbook_api.DataAccess.TableModels
{
    public class User : BaseNameEntity
    {
        public string Password { get; set; }
    }
}
