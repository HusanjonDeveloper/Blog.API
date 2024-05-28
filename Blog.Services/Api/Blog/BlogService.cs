using Blog.Common.Dtos;
using Blog.Common.Models.Blog;
using Blog.Data.Repositories;
using Blog.Services.Api.Extensions;

namespace Blog.Services.Api.Blog
{
    public class BlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IUserRepository _userRepository;

        public BlogService(IBlogRepository blogRepository, IUserRepository userRepository)
        {
            _blogRepository = blogRepository;
            _userRepository = userRepository;
        }


        public async Task<List<BlogDto>> GetAllNorRelatedBlogs(Guid userid)
        {
            await CheckUser(userid);
            var blogs = await _blogRepository.GetAll();
            return blogs.ParseModels();
        }

        public async Task<BlogDto> GetNotRelatedBlogById(Guid userid, int blogid)
        {
            await CheckUser(userid);
            var blogs = await _blogRepository.GetByid(blogid);
            return blogs.ParseToModel();
        }

        public async Task<List<BlogDto>> GetAllUserBlogs(Guid userid)
        {
            await CheckUser(userid);    
            var blogs = await _blogRepository.GetAll();
            var relatedBlogs = blogs?.Where(x => x.Userid == userid).ToList();
            return relatedBlogs.ParseModels();
        }

        public async Task<BlogDto> GetRelatedBlogById(Guid userid, int blogid)
        {
            var blog = await GetBlogById(userid, blogid);
            return blog.ParseToModel();
        }

        public async Task<BlogDto> AddBlog(Guid userid, CreateBlogModel model)
        {
            await CheckUser(userid);

            await IsExist(model.Name);

            Data.Entities.Blog blog = new()
            {
                Name = model.Name,
                Description = model.Description,
                Userid = userid,
            };
            await _blogRepository.Add(blog);
            return blog.ParseToModel();

        }

        public async Task<BlogDto> UpdateBlog(Guid userId, int blogId, UpdateBlogModel model)
        {
            var blog = await GetBlogById(userId, blogId);
            var check = false;

            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                await IsExist(model.Name);
                blog.Name = model.Name;
                check = true;
            }

            if (!string.IsNullOrEmpty(model.Description))
            {
                blog.Description = model.Description;
                check = true;
            }

            if (check) await _blogRepository.Update(blog);
            return blog.ParseToModel();
        }

        public async Task<string> DeleteBlog(Guid userid, int blogid)
        {
            var blog = await GetBlogById(userid, blogid);
            await _blogRepository.Delete(blog);
            return "Deleted successfully";
        }



        private async Task<Data.Entities.User> CheckUser(Guid userid)
        {
            var  user = await _userRepository.GetById(userid);
            return user;
        }
        private async Task IsExist(string name)
        {
            var blog = await _blogRepository.GetByName(name);
            if (blog is not null) throw new Exception($"This name \"{name}\" is already exist ");
        }
        private async Task<Data.Entities.Blog> GetBlogById(Guid userId, int blogId)
        {
            var user = await CheckUser(userId);
            var blog = user.Blogs?.FirstOrDefault(b => b.Id == blogId);
            if (blog is null) throw new Exception($"Invalid blogId \"{blogId}\"");
            return blog;
        }

    }
}
