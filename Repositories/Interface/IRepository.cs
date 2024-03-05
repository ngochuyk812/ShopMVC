﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ShopMVC.Database;
using ShopMVC.Database.Model;
using ShopMVC.Extentions;
using System.Linq.Expressions;

namespace ShopMVC.Repositories.Interface
{
    public interface IRepository<T> where T : BaseModel
    {
        DbSet<T> DbSet { get; }
        ShopContext DbContext { get; }
        T Find(Expression<Func<T, bool>> filter);
        Task<T> FindAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties = null);
        Task<IEnumerable<T>> FindListAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<Pagination<T>> PagiAsync(int index, int size, Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

    }
}
