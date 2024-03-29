﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ShopMVC.Database;
using ShopMVC.Database.Model;
using ShopMVC.Extentions;
using ShopMVC.Repositories.Interface;
using System.Diagnostics;
using System.Linq.Expressions;

namespace ShopMVC.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        public ShopContext _context;
        public DbSet<T> table;

        public Repository(ShopContext context)
        {
            _context = context;
            table = context.Set<T>();
            Debug.WriteLine($"Create Repository {table.GetType().Name}");
        }
        public DbSet<T> DbSet => table;

        public ShopContext DbContext => _context;

        public async Task<T> AddAsync(T obj)
        {
            var rs = await table.AddAsync(obj);
            return rs.Entity;
        }

        public void AddRange(IEnumerable<T> list)
        {
            table.AddRange(list);
        }

        public T Create(T obj)
        {
            table.Add(obj);
            return obj;
        }

        public void Delete(T obj)
        {
            table.Remove(obj);
        }

        public T Find(Expression<Func<T, bool>> filter)
        {
            var rs = table.Where(filter).FirstOrDefault();
            return rs;
        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties = null)
        {
            var source = table.Where(filter).Take(1);
            if (includeProperties != null)
            {
                source = includeProperties(source);
            }
            var rs = await source.FirstOrDefaultAsync();
            return rs;
        }

        public async Task<IEnumerable<T>> FindListAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            var source = table.Where(filter);
            if (includeProperties != null)
            {
                source = includeProperties(source);
            }
            var rs = await source.ToListAsync();
            return rs;
        }

        public async Task<Pagination<T>> PagiAsync(int index, int size, Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            var source = table.Where(filter);
            if (includeProperties != null)
            {
                source = includeProperties(source);
            }
            if (orderBy != null)
            {
                source = orderBy(source);
            }
            var total = source.Count();

            source = source.Skip((index - 1) * size).Take(size);
            var data = await source.ToListAsync();
            var rs = new Pagination<T>(total, index, size, data);
            return rs;
        }

        public T Update(T obj)
        {
            table.Update(obj);
            return obj;
        }


    }
}
