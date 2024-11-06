﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Unit3project.Data;
using Unit3project.Models;
using System.Security.Cryptography;
using System.Text;

namespace Unit3project.Controllers
{
    public class AccountController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Username and password cannot be empty.");
                return View();
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user != null && !string.IsNullOrEmpty(user.PasswordHash) && VerifyPassword(password, user.PasswordHash))
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new(ClaimTypes.Name, user.Username ?? string.Empty)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid username or password.");
            return View();
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Username == model.Username))
                {
                    ModelState.AddModelError("", "Username is already taken.");
                    return View(model);
                }

                var user = new User
                {
                    Username = model.Username,
                    PasswordHash = HashPassword(password: model.Password)
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        // Static method to hash a password
        private static string HashPassword(string password)
        {
            ArgumentNullException.ThrowIfNull(password); // Simplified exception throwing

            var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password)); // Use HashData for efficiency
            return Convert.ToBase64String(hashedBytes);
        }

        // Static method to verify the entered password against the stored hash
        private static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            ArgumentNullException.ThrowIfNull(storedHash); // Simplified exception throwing

            var enteredHash = HashPassword(enteredPassword);
            return enteredHash == storedHash;
        }
    }
}
