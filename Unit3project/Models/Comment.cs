using System;

namespace Unit3project.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime CommentDate { get; set; }
        public int UserId { get; set; } 
        public User? Author { get; set; }
        public int PostId { get; set; } 
    }
}
