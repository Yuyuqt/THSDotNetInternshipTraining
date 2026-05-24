using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using THSDotNetTraining.EFCoreSample.Database.AppDbContextModels;
using THSDotNetInternshipTraining.WebApi.Models;

namespace THSDotNetInternshipTraining.WebApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();

        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lst = _db.Blogs.ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("Blog not found");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogCreateRequestModel requestModel)
        {
            _db.Blogs.Add(new Blog
            {
                BlogTitle = requestModel.BlogTitle,
                BlogAuthor = requestModel.BlogAuthor,
                BlogContent = requestModel.BlogContent
            });
            var result = _db.SaveChanges();
            return StatusCode(201, new BlogCreateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Blog created successfully" : "Failed to create blog"
            });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogUpdateRequestModel requestModel)
        {
            var item = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound(new BlogUpdateResponseModel
                {
                    IsSuccess = false,
                    Message = "Blog not found"
                });
            }

            item.BlogTitle = requestModel.BlogTitle;
            item.BlogAuthor = requestModel.BlogAuthor;
            item.BlogContent = requestModel.BlogContent;

            var result = _db.SaveChanges();

            return Ok(new BlogUpdateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Blog updated successfully" : "Failed to update blog",
                Data = new BlogModel
                {
                    BlogId = item.BlogId,
                    BlogTitle = requestModel.BlogTitle,
                    BlogAuthor = requestModel.BlogAuthor,
                    BlogContent = requestModel.BlogContent
                }
            });
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogPatchRequestModel requestModel)
        {
            var item = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound(new BlogUpdateResponseModel
                {
                    IsSuccess = false,
                    Message = "Blog not found"
                });
            }

            int count = 0;
            if (!string.IsNullOrEmpty(requestModel.BlogTitle))
            {
                count++;
                item.BlogTitle = requestModel.BlogTitle;
            }
            if (!string.IsNullOrEmpty(requestModel.BlogAuthor))
            {
                count++;
                item.BlogAuthor = requestModel.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(requestModel.BlogContent))
            {
                count++;
                item.BlogContent = requestModel.BlogContent;
            }

            if (count == 0)
            {
                return NotFound(new BlogUpdateResponseModel
                {
                    IsSuccess = false,
                    Message = "Invalid data."
                });
            }

            var result = _db.SaveChanges();

            return Ok(new BlogUpdateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Blog updated successfully" : "Failed to update blog",
                Data = new BlogModel
                {
                    BlogId = item.BlogId,
                    BlogTitle = requestModel.BlogTitle,
                    BlogAuthor = requestModel.BlogAuthor,
                    BlogContent = requestModel.BlogContent
                }
            });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound(new BlogUpdateResponseModel
                {
                    IsSuccess = false,
                    Message = "Blog is not found"
                });
            }

            _db.Blogs.Remove(item);
            var result = _db.SaveChanges();

            return Ok(new BlogUpdateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Blog deleted successfully" : "Failed to delete blog"
            });
        }
    }
}