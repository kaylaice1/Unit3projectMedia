using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unit3project.Data;
using Unit3project.Models;

namespace Unit3project.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult List() =>View(_context.Posts.Include(p => p.Author).ToList());

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                post.PostDate = DateTime.Now;
                _context.Posts.Add(post);
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            return View();
        }

        public IActionResult View(int id)
        {
            var post = _context.Posts.Include(p => p.Comments).ThenInclude(c => c.Author)
                .FirstOrDefault(p => p.Id == id);
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

            return RedirectToAction("View", new { id= postId });
        }
    }
}
