using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Entities
{
    public class Post
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public string FileUrl { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        [Required]

        public string AuthorFullName { get; set; }
        public int Blogid { get; set; }
        public Blog? Blog { get; set; }

    }
}
