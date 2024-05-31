using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Context
{
    public class BlogDbContext : DbContext
    {
        public DbSet<Data.Entities.Blog> Blogs { get; set; }
        public DbSet<Data.Entities.Post> Posts { get; set; }

        public DbSet<Data.Entities.User> Users { get; set; }

        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString:"Server=LAPTOP-1FG38VDK;Database=newDbContext; Integrated Security=true;TrustServerCertificate=True;");
        }

    }

}
