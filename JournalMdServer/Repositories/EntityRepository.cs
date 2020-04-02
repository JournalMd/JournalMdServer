using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using JournalMdServer.Interfaces;
using System.Threading.Tasks;
using JournalMdServer.Models;

namespace JournalMdServer.Repositories
{
    public class EntityRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly JournalMdServerContext _context;
        private readonly DbSet<TEntity> entitiesSet;

        public EntityRepository(JournalMdServerContext context)
        {
            _context = context;

            entitiesSet = context.Set<TEntity>();
        }

        public JournalMdServerContext Context => _context;

        public IQueryable<TEntity> Query => entitiesSet;

        public async Task<TEntity> Find(params object[] keys)
        {
            return await entitiesSet.FindAsync(keys);
        }

        public void Insert(TEntity entity)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
                entitiesSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
                entitiesSet.Attach(entity);
            entry.State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
                entitiesSet.Attach(entity);
            entitiesSet.Remove(entity);
        }
    }
}
