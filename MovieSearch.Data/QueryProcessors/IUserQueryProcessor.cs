using System.Linq;
using System.Threading.Tasks;
using MovieSearch.Data.Models.User;
using MovieSearch.Core.Exceptions;

namespace MovieSearch.Data.QueryProcessors
{
    /// <summary>
    /// User CRUD operations processor.
    /// </summary>
    public interface IUserQueryProcessor
    {
        /// <summary>
        /// Gets all the users that stored in the system.
        /// </summary>
        /// <returns>Queryable list of users</returns>
        IQueryable<User> GetAllUsers();

        /// <summary>
        /// Gets a specific user by the given id.
        /// </summary>
        /// <param name="id">Id of user</param>
        /// <exception cref="NotFoundException"></exception>
        User GetUserById(int id);

        /// <summary>
        /// Creates a new user in the database.
        /// </summary>
        /// <param name="userModel">Create model of the user.</param>
        /// <exception cref="BadRequestException"></exception>
        Task CreateUser(UserModel userModel);

        /// <summary>
        /// Updates the user with the given id and new model.
        /// </summary>
        /// <param name="id">Id of user</param>
        /// <param name="userModel">Update model of the user.</param>
        Task UpdateUser(int id, UserModel userModel);

        /// <summary>
        /// Deletes the user by the given id.
        /// </summary>
        /// <param name="id">Id of user</param>
        Task DeleteUser(int id);
    }
}