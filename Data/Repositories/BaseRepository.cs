using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.Data.Models;

namespace TaskManager.Data.Repositories
{
    public class BaseRepository<T> where T : class
    {
        protected TaskManagerDBContext DbContext;
        protected DbSet<T> Set
        {
            get { return DbContext.Set<T>(); }
        }
        public BaseRepository(TaskManagerDBContext dbContext)
        {
            DbContext = dbContext;
        }

        public IQueryable<T> GetAll() 
        {
            return Set.AsNoTracking();
        }

        public IQueryable<T> GetByCondition(Expression<System.Func<T, bool>> condition)
        {
            return Set.Where(condition).AsNoTracking();
        }

        public void Add(T entity)
        {
            Set.Add(entity);
            DbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            Set.Update(entity);
            DbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            Set.Remove(entity);
            DbContext.SaveChanges();
        }

        public async Task<T> AddAsync(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("Entity cannot be null");
            }
            try
            {
                await Set.AddAsync(entity);
                await DbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                throw new Exception($"{nameof(entity)} could not be saved");
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity cannot be null");
            }
            try
            {
                Set.Update(entity);
                await DbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                throw new Exception($"{nameof(entity)} could not be updated");
            }
        }

        public async void DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity cannot be null");
            }
            try
            {
                Set.Remove(entity);
                await DbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception($"{nameof(entity)} could not be deleted");
            }
        }

    }
}
