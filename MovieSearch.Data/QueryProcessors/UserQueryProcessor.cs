using System;
using System.Linq;
using System.Threading.Tasks;
using MovieSearch.Core.Exceptions;
using MovieSearch.Data.DAL;
using MovieSearch.Data.Models.User;

namespace MovieSearch.Data.QueryProcessors
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
            return _unitOfWork.Query<User>();
        }

        public User GetUserById(int id)
        {
            User user = GetAllUsers().FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new NotFoundException("User is not found!");
            }

            return user;
        }

        public async Task CreateUser(UserModel userModel)
        {
            // There should not be same username in the system.
            // So check for any user saved with same username before.
            bool isThereAnyUserWithSameUsername = GetAllUsers().Any(u => u.Username == userModel.Username);

            if (isThereAnyUserWithSameUsername)
            {
                throw new BadRequestException("A user with the same username created before!");
            }

            var user = new User
            {
                Username = userModel.Username,
                Password = userModel.Password
            };

            _unitOfWork.Add(user);

            await _unitOfWork.CommitAsync();
        }

        public Task UpdateUser(int id, UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}