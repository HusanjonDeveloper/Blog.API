using Blog.Data.Context;
using Blog.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        // GetAll
        // GetByid
        // GetByUserName
        //Add
        // Update
        // Delete

        private readonly BlogDbContext _DbContext;

        public UserRepository(BlogDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<List<User>> GetAll()
        {
            var users = await _DbContext.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetById(Guid userid)
        {
            var user = await _DbContext.Users.FirstOrDefaultAsync(u => u.id == userid);
            if (user is null) throw new Exception("User not found ");
            return user;
        }

        public async Task<User?> GetByUsername(string Username)
        {
            var user = await _DbContext.Users.FirstOrDefaultAsync(u => u.UserName == Username);
            return user;
        }

        public async Task Add(User user)
        {
            _DbContext.Users.Add(user);
            await _DbContext.SaveChangesAsync();
           
        }
        public async Task Update(User user)
        {
           _DbContext.Users.Update(user);
            await _DbContext.SaveChangesAsync();
        }

        public async Task Delete(User user)
        {
            _DbContext?.Users.Remove(user);
            await _DbContext.SaveChangesAsync();
        }


    }
}
