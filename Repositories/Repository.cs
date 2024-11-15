using Interview_Server.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Interview_Server.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        protected readonly DatabaseContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> deleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                throw new Exception("Entity not found");
            }
            _dbSet.Remove(entity);
             await _context.SaveChangesAsync();
            return entity;
            }

        public async Task<TEntity> EditAsync(TEntity entity)
        {
           _dbSet.Update(entity);
            await _context.SaveChangesAsync();  
            return entity;
           }

        public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties) { 
            IQueryable<TEntity> query = _dbSet.Where(predicate);
            foreach(var includeProperty in includeProperties)
            {
               query = query.Include(includeProperty);
            }   
            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach( var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            var idProperty = typeof(TEntity).GetProperty("Id"); 
            if (idProperty == null)
            {
                throw new InvalidOperationException("Entity does not have an Id property.");
            }

            var entity = await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
            return entity;
        }

        
    }
}
