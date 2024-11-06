using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unit3project.Data;
using Unit3project.Models;

namespace Unit3project.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult Profile(int id)
        {
            var user = _context.Users.Include(u => u.Posts).FirstOrDefault(u => u.UserId == id);
            return View(user);
        }
    }
}
