using Blog.Data.Context;
using Blog.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        // GetAll
        // GetById
        //Add
        // Update
        // Delete

        private readonly BlogDbContext _dbContext;

        public PostRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Post>> GetAll()
        {
            var post = await _dbContext.Posts.ToListAsync();
            return post;
        }
        public async Task<Post> GetById(int userId)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == userId);
            if (post is null) throw new Exception("user not found");
            return post;
        }
        public async Task Add(Post post)
        {
            _dbContext.Posts.Add(post);
            await _dbContext.SaveChangesAsync();
        }
        public async Task Update(Post post)
        {
            _dbContext.Posts.Update(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Post post)
        {
            _dbContext.Remove(post);
            await _dbContext.SaveChangesAsync();
        }



    }


}
