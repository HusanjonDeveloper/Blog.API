using Blog.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Context
{
    public class BlogDbContext : DbContext
    {
        public DbSet<Entities.Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DbSet<User> Users { get; set; }

        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

    }

}
