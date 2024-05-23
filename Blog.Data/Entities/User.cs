using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Entities
{
    public class User
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? PhotoUrl { get; set; }
        public virtual List<Blog?> Blogs { get; set; }
    }
}
