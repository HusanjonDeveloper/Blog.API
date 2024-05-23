using System.ComponentModel.DataAnnotations;

namespace Blog.Common.Models.User
{
    public class UpdateUserModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
    }
}
