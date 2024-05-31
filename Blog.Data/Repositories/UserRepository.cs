using Blog.Data.Context;
using Blog.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        // GetAll
        // GetById
        // GetByUserName
        //Add
        // Update
        // Delete

        private readonly BlogDbContext _dbContext;

        public UserRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAll()
        {
            var users = await _dbContext.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetById(Guid userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user is null) throw new Exception("User not found ");
            return user;
        }

        public async Task<User?> GetByUsername(string UserName)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == UserName);
            return user;
        }

        public async Task Add(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

        }
        public async Task Update(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(User user)
        {
            _dbContext?.Users.Remove(user);
            await _dbContext!.SaveChangesAsync();
        }


    }
}
