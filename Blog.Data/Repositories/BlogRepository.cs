
using Blog.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        // GetAll
        // GetByid
        // GetByName
        //Add
        // Update
        // Delete

        private readonly BlogDbContext _DbContext;

        public BlogRepository(BlogDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<List<Entities.Blog>> GetAll()
        {
            var blog = await _DbContext.Blogs.ToListAsync();
            return blog;
        }

        public async Task<Entities.Blog> GetByid(int id)
        {
            var blog = await _DbContext.Blogs.FirstOrDefaultAsync(b => b.Id == id);
            if (blog is null) throw new Exception("id not found");
            return blog;
        }
        public async Task<Entities.Blog?> GetByName(string name)
        {
            var blog = await _DbContext.Blogs.FirstOrDefaultAsync(b => b.Name.ToLower() == name.ToLower());
            return blog;
        }
        public async Task Add(Entities.Blog blog)
        {
            _DbContext.Blogs.Add(blog);
            await _DbContext.SaveChangesAsync();
        }

        public async Task Update(Entities.Blog blog)
        {
            _DbContext.Update(blog);
            await _DbContext.SaveChangesAsync();
        }

        public async Task Delete(Entities.Blog blog)
        {
            _DbContext.Remove(blog);
            await _DbContext.SaveChangesAsync();
        }


    }
}
