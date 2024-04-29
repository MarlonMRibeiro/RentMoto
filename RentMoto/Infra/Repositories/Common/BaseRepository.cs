using Domain.Base;
using Domain.Repositories.Common;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Common
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly RentMotoContext _context;

        protected BaseRepository(RentMotoContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().Where(w => !w.DeleteAt.HasValue);
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            SaveChanges();
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            SaveChanges();
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
        }

        public void Remove(T entity)
        {
            entity.DeleteAt = DateTime.UtcNow;
            _context.Set<T>().Update(entity);
            SaveChanges();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }


        public T GetById(Guid Id)
        {
            return _context.Set<T>().FirstOrDefault(f => f.Id == Id && !f.DeleteAt.HasValue);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
