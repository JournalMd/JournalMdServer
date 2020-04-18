using JournalMdServer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public JournalMdServerContext Context { get; }

        IQueryable<TEntity> Query { get; }

        Task<TEntity> Find(params object[] keys);

        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
