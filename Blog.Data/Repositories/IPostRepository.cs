using Blog.Data.Entities;

namespace Blog.Data.Repositories
{
    public interface IPostRepository
    {
        // GetAll
        // GetByid
        //Add
        // Update
        // Delete
        Task<List<Post>> GetAll();
        Task<Post> GetById(int userid);
        Task Add(Post post);
        Task Update(Post post);
        Task Delete(Post post);
    }
}
