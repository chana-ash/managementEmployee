using Manage.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static Manage.Data.Repositories.Repository;

namespace Manage.Data.Repositories
{
   
    
        public class Repository<T> : IRepository<T> where T : class
        {
            private readonly DbSet<T> _dbSet;
            private readonly DataContext _context;


        public Repository(DataContext context)
            {
                _dbSet = context.Set<T>();
                _context = context;
            }

            public async Task<T> AddAsync(T entity)
            {
                 _dbSet.Add(entity);
                 await _context.SaveChangesAsync();
            return entity;
            }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }


        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

    }

}
