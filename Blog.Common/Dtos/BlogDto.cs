using Blog.Data.Entities;

namespace Blog.Common.Dtos
{
    public class BlogDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual User? User { get; set; }

        public virtual List<PostDto>? Posts { get; set; }
    }
}
