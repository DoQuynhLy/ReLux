using Microsoft.EntityFrameworkCore;
using ReLux.DataAccess.Data;
using ReLux.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReLux.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        public DbSet<T> dbset;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            //FoodType, Category
            //_db.MenuItem.Include(u => u.FoodType).Include(u => u.Category);
            //_db.MenuItem.OrderBy(u => u.Name);
            this.dbset = _db.Set<T>();
        }

        public void Add(T entity)
        {
            dbset.Add(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderby = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                // abc,,xyz -> abc xyz
                foreach (var includeProperty in includeProperties.Split(
                   new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries
                ))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (orderby != null)
            {
                return orderby(query).ToList();
            }
            return query.ToList();
        }
        public T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                // abc,,xyz -> abc xyz
                foreach (var includeProperty in includeProperties.Split(
                   new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries
                ))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbset.RemoveRange(entities);
        }
    }
}
