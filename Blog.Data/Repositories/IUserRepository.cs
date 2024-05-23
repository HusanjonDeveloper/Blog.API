using Blog.Data.Entities;

namespace Blog.Data.Repositories
{
    public interface IUserRepository
    {
        // GetAll
        // GetByid
        // GetByUserName
        //Add
        // Update
        // Delete

        Task<List<User>> GetAll();
        Task<User> GetById(Guid userid);
        Task<User?> GetByUsername(string Username);
        Task Add(User user);
        Task Update(User user);
        Task Delete(User user);

    }
}
