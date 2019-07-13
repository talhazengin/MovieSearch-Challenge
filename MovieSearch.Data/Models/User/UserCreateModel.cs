using System.ComponentModel.DataAnnotations;

namespace MovieSearch.Data.Models.User
{
    public class UserCreateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
