using Blog.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Context
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString:"BlogDbContext");
        }

        public DbSet<Data.Entities.User> Users { get; set; }
        public DbSet<Data.Entities.Blog> Blogs { get; set; }
        public DbSet<Data.Entities.Post> Posts { get; set; }



    }

}
