namespace Blog.Common.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? PhotoUrl { get; set; }

        public virtual List<BlogDto>? Blogs { get; set; }
    }
}
