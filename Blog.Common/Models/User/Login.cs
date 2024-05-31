using System.ComponentModel.DataAnnotations;

namespace Blog.Common.Models.User
{
    public class Login
    {
        [Required]
        public required string UserName { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
