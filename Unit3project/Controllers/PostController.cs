using Microsoft.AspNetCore.Mvc;
using Unit3project.Data;
using Unit3project.Models;
using System;
using System.Linq;

namespace Unit3project.Controllers
{
    public class PostController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        public IActionResult List()
        {
            var posts = _context.Posts.ToList();
            return View(posts);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Post post)
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                if (int.TryParse(userIdString, out int userId))
                {
                    post.UserId = userId;
                    post.PostDate = DateTime.Now;
                    _context.Posts.Add(post);
                    _context.SaveChanges();
                    return RedirectToAction("List");
                }
            }

            ModelState.AddModelError("", "You must be logged in to create a post.");
            return View(post);
        }

        public IActionResult View(int id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);

            if (post != null)
            {
                var comments = _context.Comments
                    .Where(c => c.PostId == id)
                    .ToList();
                ViewBag.Comments = comments;
            }

            return View(post);
        }

        [HttpPost]
        public IActionResult AddComment(int postId, string content)
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                var userIdstring = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                if (int.TryParse(userIdstring, out int userId))
                {
                    var comment = new Comment
                    {
                        Content = content,
                        CommentDate = DateTime.Now,
                        UserId = userId,
                        PostId = postId
                    };

                    _context.Comments.Add(comment);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("View", new { id = postId });
        }
    }
}
