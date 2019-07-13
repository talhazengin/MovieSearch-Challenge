using Microsoft.EntityFrameworkCore.Storage;

namespace MovieSearch.Data.DAL
{
    public class Transaction : ITransaction
    {
        private readonly IDbContextTransaction _efTransaction;

        public Transaction(IDbContextTransaction efTransaction)
        {
            _efTransaction = efTransaction;
        }

        public void Commit()
        {
            _efTransaction.Commit();
        }

        public void Rollback()
        {
            _efTransaction.Rollback();
        }

        public void Dispose()
        {
            _efTransaction.Dispose();
        }
    }
}