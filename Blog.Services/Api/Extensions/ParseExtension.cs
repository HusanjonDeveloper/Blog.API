using Blog.Common.Dtos;
using Mapster;

namespace Blog.Services.Api.Extensions
{
    public static class ParseExtension
    {
        public static UserDto ParsToModel(this Data.Entities.User user)
        {
            return new UserDto()
            {
                Id = user.Id,
                FirstName = user.Firstname,
                LastName = user.Lastname,
                UserName = user.Username,
                CreatedAt = user.CreatedAt,
                PhotoUrl = user.PhotoUrl
            };
        }

        public static List<UserDto> ParsToModels(this List<Data.Entities.User>? users)
        {
            if (users == null || users.Count == 0) return new List<UserDto>();
            var parsToModels = new List<UserDto>();

            foreach (var user in users)
            {

                parsToModels.Add(user.ParsToModel());
            }
            return parsToModels;
        }

        public static BlogDto ParseToModel(this Data.Entities.Blog blog)
        {
            BlogDto blogDto = blog.Adapt<BlogDto>();
            return blogDto;
        }

        public static List<BlogDto> ParseModels(this List<Data.Entities.Blog>? blogs)
        {
            var dos = new List<BlogDto>();
            if (blogs == null || blogs.Count == 0) return new List<BlogDto>();
            foreach (var blog in blogs)
            {
                dos.Add(blog.ParseToModel());
            }

            return dos;
        }

        public static PostDto ParseToModel(this Data.Entities.Post post)
        {
            PostDto postDto = post.Adapt<PostDto>();
            return postDto;
        }

        public static List<PostDto> ParseModels(this List<Data.Entities.Post>? posts)
        {
            var dos = new List<PostDto>();
            if (posts == null || posts.Count == 0) return new List<PostDto>();
            foreach (var post in posts)
            {
                dos.Add(post.ParseToModel());
            }
            return dos;
        }
    }
}
