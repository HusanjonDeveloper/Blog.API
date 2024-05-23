namespace Blog.Data.Repositories
{
    public interface IBlogRepository
    {
        // GetAll
        // GetByid
        // GetByName
        //Add
        // Update
        // Delete
        Task<List<Entities.Blog>> GetAll();
        Task<Entities.Blog> GetByid(int id);
        Task<Entities.Blog?> GetByUsername(string name);
        Task Add(Entities.Blog blog);
        Task Update(Entities.Blog blog);
        Task Delete(Entities.Blog blog);
    }
}
