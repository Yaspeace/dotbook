using dotbook_api.DataAccess.Context;
using dotbook_api.DataAccess.TableModels;
using dotbook_api.Models.Base;
using dotbook_api.Services.Base;
using System.Collections.Generic;
using System.Linq;

namespace dotbook_api.Services
{
    public class PublishService : BaseCrudService<Publish>
    {
        public PublishService(DotbookContext context) : base(context) { }

        public IEnumerable<Publish> Search(BaseQueryParams filter = null, string search = "")
        {
            return this.Get(filter).Where(x => x.Name.ToLower().Contains(search.ToLower())).ToArray();
        }
    }
}
