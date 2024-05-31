namespace Blog.Data.Repositories
{
    public interface IBlogRepository
    {
        // GetAll
        // GetById
        // GetByName
        //Add
        // Update
        // Delete
        Task<List<Entities.Blog>> GetAll();
        Task<Entities.Blog> GetById(int Id);
        Task<Entities.Blog?> GetByName(string Name);
        Task Add(Entities.Blog blog);
        Task Update(Entities.Blog blog);
        Task Delete(Entities.Blog blog);
    }
}
