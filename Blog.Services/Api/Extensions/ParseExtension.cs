﻿using Blog.Common.Dtos;
using Mapster;

namespace Blog.Services.Api.Extensions
{
    public static class ParseExtension
    {
        public static UserDto ParsToModel(this Data.Entities.User user)
        {
            return new UserDto()
            {
                id = user.id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                CreatedAt = user.CreatedAt,
                PhotoUrl = user.PhotoUrl
            };
        }

        public static List<UserDto> ParsToModels(this List<Data.Entities.User>? users)
        {
            if (users == null || users.Count == 0) return new List<UserDto>();
            var dtos = new List<UserDto>();
            foreach (var user in users)
            {

                dtos.Add(user.ParsToModel());
            }
            return dtos;
        }

        public static BlogDto ParseToModel(this Data.Entities.Blog blog)
        {
            BlogDto blogDto = blog.Adapt<BlogDto>();
            return blogDto;
        }

        public static List<BlogDto> ParseModels(this List<Data.Entities.Blog>? blogs)
        {
            var dtos = new List<BlogDto>();
            if (blogs == null || blogs.Count == 0) return new List<BlogDto>();
            foreach (var blog in blogs)
            {
                dtos.Add(blog.ParseToModel());
            }

            return dtos;
        }

        public static PostDto ParseToModel(this Data.Entities.Post post)
        {
            PostDto postDto = post.Adapt<PostDto>();
            return postDto;
        }

        public static List<PostDto> ParseModels(this List<Data.Entities.Post>? posts)
        {
            var dtos = new List<PostDto>();
            if (posts == null || posts.Count == 0) return new List<PostDto>();
            foreach (var post in posts)
            {
                dtos.Add(post.ParseToModel());
            }
            return dtos;
        }
    }
}