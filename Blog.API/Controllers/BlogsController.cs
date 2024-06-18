using Blog.Common.Models.Blog;
using Blog.Services.Api;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/users/{userId:guid}/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly BlogService _blogService;

        public BlogsController(BlogService blogService)
        {
            _blogService = blogService;
        }

        // This Not Releted Blogs
        [HttpGet("not-related-blogs")]
        public async Task<IActionResult> GetAllNotRelatedBlogs(Guid userId)
        {
            try
            {
                var blog = await _blogService.GetAllNotReletedBlogs(userId);
                return Ok(blog);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("not-related-blogs/{blogId:int}")]
        public async Task<IActionResult> GetAllNotRelatedBlogById(Guid userId, int blogId)
        {
            try
            {
                var blog = await _blogService.GetNotRelatedBlogById(userId, blogId);
                return Ok(blog);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // This Releted Blogs
        [HttpGet]
        public async Task<IActionResult> GetAllUsersBlogs(Guid userId)
        {
            try
            {
                var blog = await _blogService.GetAllUserBlogs(userId);
                return Ok(blog);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("{blogId:int}")]
        public async Task<IActionResult> GetUserBlogById(Guid userId, int blogId)
        {
            try
            {
                var blog = await _blogService.GetRelatedBlogById(userId, blogId);
                return Ok(blog);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("/AddBlog")]
        public async Task<IActionResult> AddUserBlog(Guid userId, [FromBody] CreateBlogModel model)
        {
            try
            {
                var blog = await _blogService.AddBlog(userId, model);
                return Ok(blog);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{blogId:int}")]
        public async Task<IActionResult> UpdateUserBlog(Guid userId, int blogId, UpdateBlogModel model)
        {
            try
            {
                var blog = await _blogService.UpdateBlog(userId, blogId, model);
                return Ok(blog);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserBlog(Guid userId, int blogId)
        {
            try
            {
                var blog = await _blogService.DeleteBlog(userId, blogId);
                return Ok(blog);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
