﻿using Blog.Common.Models.Post;
using Blog.Services.Api;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/users/{userId:guid}/blogs/{blogId:int}/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly  PostService _postService;

        public PostsController(PostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserPosts(Guid userId, int blogId)
        {
            try
            {
                var post = await _postService.GetAllPosts(userId, blogId);
                return Ok(post);    
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{postId:int}")]
        public async Task<IActionResult> GetAllUserPostsById(Guid userId, int blogId, int postId)
        {
            try
            {
                var post = await _postService.GetPostById(userId, blogId, postId);
                return Ok(post);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{AddPost}")]
        public async Task<IActionResult> AddUserPost(Guid userId, int blogId, [FromBody] CreatePostModel model)
        {
            try
            {
                var post = await _postService.AddPost(userId, blogId, model);
                return Ok(post);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{UpdatePost:int}")]
        public async Task<IActionResult> UpdatePost(Guid userId, int blogId, int postId,
            [FromBody] UpdatePostModel model)
        {
            try
            {
                var post = await _postService.UpdatePost(userId, blogId, postId, model);
                return Ok(post);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{deletePost}")]
        public async Task<IActionResult> DeletePost(Guid userId, int blogId, int postId)
        {
            try
            {
                var post = await _postService.DeletePost(userId, blogId, postId);
                return Ok(post);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
