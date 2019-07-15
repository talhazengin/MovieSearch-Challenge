using System;
using System.ComponentModel.DataAnnotations;

namespace MovieSearch.Data.Models.User
{
    [Serializable]
    public class UserModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
