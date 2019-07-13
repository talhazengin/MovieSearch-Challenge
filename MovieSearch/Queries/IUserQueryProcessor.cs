using System.Linq;
using MovieSearch.Data.Models;
using MovieSearch.Data.Models.User;

namespace MovieSearch.Queries
{
    public interface IUserQueryProcessor
    {
        /// <summary>
        /// Gets all the users that stored in the system.
        /// </summary>
        /// <returns>Queryable list of users</returns>
        IQueryable<User> GetAllUsers();
    }
}