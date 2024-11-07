using Microsoft.AspNetCore.Mvc;
using Unit3project.Data;
using Unit3project.Models;
using System.Linq;

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
            var recentPosts = _context.Posts
                .OrderByDescending(p => p.PostDate)
                .Take(5)
                .ToList();

            return View(recentPosts);
        }

        public IActionResult RecentPosts()
        {
            var recentPosts = _context.Posts
                .OrderByDescending(p => p.PostDate)
                .Take(5)
                .ToList();

            return PartialView("Home/_RecentPosts", recentPosts);
        }
    }
}
