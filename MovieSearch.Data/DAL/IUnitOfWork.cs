using System;
using System.Linq;
using System.Threading.Tasks;

namespace MovieSearch.Data.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IQueryable<T> Query<T>() where T : class;

        void Add<T>(T obj) where T : class;

        void Update<T>(T obj) where T : class;

        void Remove<T>(T obj) where T : class;

        void Attach<T>(T obj) where T : class;

        ITransaction BeginTransaction();

        void Commit();

        Task CommitAsync();
    }
}