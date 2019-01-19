using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Antl.WebServer.Entities;
using Antl.WebServer.Infrastructure;
using Antl.WebServer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Antl.WebServer.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly AntlContext _context;
        private readonly DbSet<TEntity> _set;

        public GenericRepository(AntlContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _set = _context.Set<TEntity>() ?? throw new ArgumentNullException(nameof(TEntity));
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await _set.AddAsync(entity).ConfigureAwait(false);
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _set.Remove(entity);
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            return _set.SingleOrDefaultAsync(where);
        }
        public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> where)
        {
            return _set.Where(where).ToListAsync();
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return _set.ToListAsync();
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _set.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }
    }
}