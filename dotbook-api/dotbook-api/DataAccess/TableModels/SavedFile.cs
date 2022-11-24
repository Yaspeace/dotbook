using dotbook_api.DataAccess.TableModels.Base;

namespace dotbook_api.DataAccess.TableModels
{
    public class SavedFile : BaseNameEntity
    {
        public string Suffix { get; set; }
    }
}
