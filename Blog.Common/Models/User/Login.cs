using System.ComponentModel.DataAnnotations;

namespace Blog.Common.Models.User
{
    public class Login
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
