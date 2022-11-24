using dotbook_api.DataAccess.Context;
using dotbook_api.DataAccess.TableModels.Base;
using dotbook_api.Extensions;
using dotbook_api.Models.Base;
using System;
using System.Linq;

namespace dotbook_api.Services.Base
{
    public class BaseReadService<T> 
        where T : BaseEntity
    {
        protected readonly DotbookContext _context;

        public BaseReadService(DotbookContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение сущностей по фильтру
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns>Queryable коллекция сущностей</returns>
        public IQueryable<T> Get(BaseQueryParams filter = null)
        {
            return _context.Set<T>().AsQueryable().Filter(filter);
        }

        /// <summary>
        /// Получение сущности по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Сущность</returns>
        public T GetById(int id)
        {
            var entity = Get().Where(x => x.Id == id).FirstOrDefault();
            if (entity == null) throw new Exception($"Сущность с id: {id} не найдена");
            return entity;
        }
    }
}
