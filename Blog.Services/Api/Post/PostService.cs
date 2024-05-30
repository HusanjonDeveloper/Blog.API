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
        public async Task<PostDto> GetPostById(int postid)
        {
            var posts = await _postRepository.GetAll();
            var post = posts.FirstOrDefault(x => x.id == postid);
            if (post is null) throw new Exception($"The post is not found with \"{postid}\"");
            return post.ParseToModel();

        }

        public async Task<List<PostDto>> GetAllPosts(Guid userid, int blogid)
        {
            var posts = await FilteredPosts(userid, blogid);
            return posts.ParseModels();
        }

        public async Task<PostDto> GetPostById(Guid userid, int blogid, int postid)
        {
            var posts = await CheckPost(userid, blogid, postid);
            return posts.ParseToModel();
        }

        public async Task<PostDto> AddPost(Guid userid, int blogid, CreatePostModel model)
        {
            var user = CheckUser(userid);
            await CheckBlog(userid,blogid);

            var post = new Data.Entities.Post()
            {
               Title = model.Title,
               Content = model.Content,
               AuthorFullName = $"{user} {user}", // ustozdan soreman 
               Blogid = blogid,
            };

            await _postRepository.Add(post);
            return post.ParseToModel();
        }


        public async Task<PostDto> UpdatePost(Guid userid, int blogid, int postid, UpdatePostModel model)
        {
            var post = await CheckPost(userid,blogid,postid);
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

        public async Task<string> DeletePost(Guid userid, int blogid, int postid)
        {
            var post = await CheckPost(userid,blogid,postid);
            await _postRepository.Delete(post);
            return "Deleted successfully";
        }

        private async Task<List<Data.Entities.Post>?> FilteredPosts(Guid userid, int blogid)
        {
            var blog = await CheckBlog(userid, blogid);
            var filteredPosts = blog.Posts?.Where(post => post.id == blogid).ToList();
            return filteredPosts;
        }

        private async Task<Data.Entities.User> CheckUser(Guid userId)
        {
            var user = await _userRepository.GetById(userId);
            return user;
        }

        private async Task<Data.Entities.Blog> CheckBlog(Guid userid, int blogid)
        {
            var user = await CheckUser(userid);
            var blog = user.Blogs?.FirstOrDefault(blog => blog.Id == blogid);
            if (blog is null) throw new Exception($"Not found blog with \"{blogid}\"");
            return blog;
        }

        private async Task<Data.Entities.Post> CheckPost(Guid userid, int blogid, int postid)
        {
            var blog = await CheckBlog(userid, blogid);
            var post = blog.Posts?.FirstOrDefault(p => p.id == postid);
            if (post is null) throw new Exception($"The post is not found with \"{postid}\"");
            return post;
        }
    }
}
