using System;
using System.Collections.Generic;

namespace Unit3project.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string?  PasswordHash { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<Post>? Posts { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}