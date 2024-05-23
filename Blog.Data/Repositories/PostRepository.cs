using Blog.Data.Context;
using Blog.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        // GetAll
        // GetByid
        //Add
        // Update
        // Delete

        private readonly BlogDbContext _DbContext;

        public PostRepository(BlogDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<List<Post>> GetAll()
        {
            var post = await _DbContext.Posts.ToListAsync();
            return post;
        }
        public async Task<Post> GetById(int id)
        {
            var post = await _DbContext.Posts.FirstOrDefaultAsync(p => p.id == id);
            if (post is null) throw new Exception("user not found");
            return post;
        }
        public async Task Add(Post post)
        {
            _DbContext.Posts.Add(post);
            await _DbContext.SaveChangesAsync();
        }
        public async Task Update(Post post)
        {
            _DbContext.Posts.Update(post);
            await _DbContext.SaveChangesAsync();
        }

        public async Task Delete(Post post)
        {
            _DbContext.Remove(post);
            await _DbContext.SaveChangesAsync();
        }



    }


}
