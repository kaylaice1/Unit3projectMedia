using Microsoft.AspNetCore.Mvc;
using Unit3project.Data;
using Unit3project.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Unit3project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var posts = _context.Posts
                .Include(p => p.Author)
                .OrderByDescending(p => p.PostDate)
                .ToList();

            return View(posts);
        }
    }
}
