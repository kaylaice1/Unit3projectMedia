using Microsoft.EntityFrameworkCore;
using Unit3project.Models;

namespace Unit3project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Username = "admin", Email = "admin@example.com", PasswordHash = "hashedpassword", DateCreated = DateTime.Now }
            );

            modelBuilder.Entity<Post>().HasData(
                new Post { Id = 1, Title = "Welcome Post", Content = "This is the first post", PostDate = DateTime.Now, UserId = 1 }
            );

            modelBuilder.Entity<Comment>().HasData(
                new Comment { Id = 1, Content = "First comment!", CommentDate = DateTime.Now, UserId = 1, PostId = 1 }
            );
        }
    }
}
