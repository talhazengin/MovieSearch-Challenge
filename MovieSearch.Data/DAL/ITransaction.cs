using System;

namespace MovieSearch.Data.DAL
{
    public interface ITransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}
