
using BookService.Application.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookService.Application.Data
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly BookManagementContext _dbContext;
        protected DbSet<T> DbSet;
        
        public Repository(BookManagementContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<T> InsertAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            entity.CreatedDate = DateTime.Now;

            await DbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            entity.ModifiedDate = DateTime.Now;

            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            DbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
