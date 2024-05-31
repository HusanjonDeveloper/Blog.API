
using Blog.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        // GetAll
        // GetById
        // GetByName
        //Add
        // Update
        // Delete

        private readonly BlogDbContext _dbContext;

        public BlogRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Entities.Blog>> GetAll()
        {
            var blog = await _dbContext.Blogs.ToListAsync();
            return blog;
        }

        public async Task<Entities.Blog> GetById(int Id)
        {
            var blog = await _dbContext.Blogs.FirstOrDefaultAsync(b => b.Id == Id);
            if (blog is null) throw new Exception("Id not found");
            return blog;
        }
        public async Task<Entities.Blog?> GetByName(string Name)
        {
            var blog = await _dbContext.Blogs.FirstOrDefaultAsync(b => b.Name.ToLower() == Name.ToLower());
            return blog;
        }
        public async Task Add(Entities.Blog blog)
        {
            _dbContext.Blogs.Add(blog);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Entities.Blog blog)
        {
            _dbContext.Update(blog);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Entities.Blog blog)
        {
            _dbContext.Remove(blog);
            await _dbContext.SaveChangesAsync();
        }


    }
}
