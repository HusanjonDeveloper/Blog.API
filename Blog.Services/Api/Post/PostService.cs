using Blog.Common.Dtos;
using Blog.Common.Models.Post;
using Blog.Data.Repositories;
using Blog.Services.Api.Extensions;

namespace Blog.Services.Api.Post
{
    public class PostService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;

        public PostService(IBlogRepository blogRepository, IUserRepository userRepository, IPostRepository postRepository)
        {
            _blogRepository = blogRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        public async Task<List<PostDto>> GetAllPosts()
        {
            var allPosts = await _postRepository.GetAll();
            return allPosts.ParseModels();
        }
        public async Task<PostDto> GetPostById(int postId)
        {
            var posts = await _postRepository.GetAll();
            var post = posts.FirstOrDefault(x => x.id == postId);
            if (post is null) throw new Exception($"The post is not found with \"{postId}\"");
            return post.ParseToModel();

        }

        public async Task<List<PostDto>> GetAllPosts(Guid userId, int blogId)
        {
            var posts = await FilteredPosts(userId, blogId);
            return posts.ParseModels();
        }

        public async Task<PostDto> GetPostById(Guid userId, int blogId, int postId)
        {
            var posts = await CheckPost(userId, blogId, postId);
            return posts.ParseToModel();
        }

        public async Task<PostDto> AddPost(Guid userId, int blogId, CreatePostModel model)
        {
            var user = await  CheckUser(userId);
            await CheckBlog(userId,blogId);

            var post = new Data.Entities.Post()
            {
               Title = model.Title,
               Content = model.Content,
               AuthorFullName = $"{user.FirstName} {user.LastName}", 
               Blogid = blogId,
            };

            await _postRepository.Add(post);
            return post.ParseToModel();
        }


        public async Task<PostDto> UpdatePost(Guid userId, int blogId, int postId, UpdatePostModel model)
        {
            var post = await CheckPost(userId,blogId,postId);
            var check = false;
            
            if(!string.IsNullOrWhiteSpace(model.Title))
            {
                post.Title = model.Title;
                check = true;
            }
            if (!string.IsNullOrWhiteSpace(model.Content))
            { 
              post.Content = model.Content;
                check = true;
            }
            if(check)
                await _postRepository.Update(post);
            return post.ParseToModel();
        }

        public async Task<string> DeletePost(Guid userId, int blogId, int postId)
        {
            var post = await CheckPost(userId,blogId,postId);
            await _postRepository.Delete(post);
            return "Deleted successfully";
        }

        private async Task<List<Data.Entities.Post>?> FilteredPosts(Guid userId, int blogId)
        {
            var blog = await CheckBlog(userId, blogId);
            var filteredPosts = blog.Posts?.Where(post => post.id == blogId).ToList();
            return filteredPosts;
        }

        private async Task<Data.Entities.User> CheckUser(Guid userId)
        {
            var user = await _userRepository.GetById(userId);
            return user;
        }

        private async Task<Data.Entities.Blog> CheckBlog(Guid userId, int blogId)
        {
            var user = await CheckUser(userId);
            var blog = user.Blogs.FirstOrDefault(blog => blog.Id == blogId);
            if (blog is null) throw new Exception($"Not found blog with \"{blogId}\"");
            return blog;
        }

        private async Task<Data.Entities.Post> CheckPost(Guid userId, int blogId, int postId)
        {
            var blog = await CheckBlog(userId, blogId);
            var post = blog.Posts?.FirstOrDefault(p => p.id == postId);
            if (post is null) throw new Exception($"The post is not found with \"{postId}\"");
            return post;
        }
    }
}
