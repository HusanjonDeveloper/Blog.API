using Blog.Data.Entities;

namespace Blog.Data.Repositories
{
    public interface IUserRepository
    {
        // GetAll
        // GetById
        // GetByUserName
        //Add
        // Update
        // Delete

        Task<List<User>> GetAll();
        Task<User> GetById(Guid userId);
        Task<User?> GetByUsername(string UserName);
        Task Add(User user);
        Task Update(User user);
        Task Delete(User user);

    }
}
