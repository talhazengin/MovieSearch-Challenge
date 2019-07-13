using System;
using System.Linq;
using MovieSearch.Data.DAL;
using MovieSearch.Data.Models;
using MovieSearch.Data.Models.User;

namespace MovieSearch.Queries
{
    public class UserQueryProcessor : IUserQueryProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserQueryProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}
