using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieSearch.Data.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(MovieSearchDbContext context)
        {
            Context = context;
        }

        public MovieSearchDbContext Context { get; private set; }

        public IQueryable<T> Query<T>()
            where T : class
        {
            return Context.Set<T>();
        }

        public void Add<T>(T obj)
            where T : class
        {
            DbSet<T> set = Context.Set<T>();
            set.Add(obj);
        }

        public void Update<T>(T obj)
            where T : class
        {
            DbSet<T> set = Context.Set<T>();
            set.Attach(obj);
            Context.Entry(obj).State = EntityState.Modified;
        }

        void IUnitOfWork.Remove<T>(T obj)
        {
            DbSet<T> set = Context.Set<T>();
            set.Remove(obj);
        }

        public void Attach<T>(T newUser) where T : class
        {
            DbSet<T> set = Context.Set<T>();
            set.Attach(newUser);
        }

        public ITransaction BeginTransaction()
        {
            return new Transaction(Context.Database.BeginTransaction());
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context = null;
        }
    }
}