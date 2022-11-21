using dotbook_api.DataAccess.Context;
using dotbook_api.DataAccess.TableModels.Base;
using System;
using Microsoft.EntityFrameworkCore;

namespace dotbook_api.Services.Base
{
    public class BaseCrudService<T> : BaseReadService<T> 
        where T : BaseEntity
    {
        public BaseCrudService(DotbookContext context) : base(context) { }

        /// <summary>
        /// Изменить сущность. Сохранение БД не производится.
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <returns>Измененная сущность</returns>
        public T Update(T entity)
        {
            if (entity.Id <= 0) throw new Exception("Некорректный идентификатор сущности");

            var entry = _context.Update(entity);
            return entry.Entity;
        }

        /// <summary>
        /// Добавить сущность. Сохранение БД не производится
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <returns>Добавленная сущность</returns>
        public T Add(T entity)
        {
            if (entity.Id != 0) throw new Exception("Некорректный идентификатор сущности");

            var entry = _context.Set<T>().Add(entity);
            return entry.Entity;
        }

        /// <summary>
        /// Изменить или добавить сущность.<br/>
        /// Если entity.Id == 0, будет производиться добавление, иначе - изменение<br/>
        /// Сохранение БД не производится
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <returns>Добавленная/измененная сущность</returns>
        public T AddOrUpdate(T entity)
        {
            if (entity == null) throw new ArgumentNullException("Сущность");
            
            if (entity.Id == 0)
            {
                return Add(entity);
            }
            else
            {
                return Update(entity);
            }
        }

        /// <summary>
        /// Удаление сущности с указанным идентификатором. Сохранение БД не производится.
        /// </summary>
        /// <param name="id">Идентификатор</param>
        public void Delete(int id)
        {
            var entity = _context.Set<T>().Find(id) ?? throw new Exception($"Сущность с id: {id} не найдена");
            var entry = _context.Entry(entity);
            entry.State = EntityState.Deleted;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
