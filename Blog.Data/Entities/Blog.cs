using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Entities
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public Guid Userid { get; set; }
        public User? User { get; set; }
        public List<Post>? Posts { get; set; }
    }
}
