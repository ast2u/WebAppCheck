using Microsoft.EntityFrameworkCore;

namespace WebAppCheck.Models
{
    public class PostDbContext:DbContext
    {
        public PostDbContext(DbContextOptions<PostDbContext>options):base(options)
        {
            
        }
        public DbSet<Post> Posts { get; set; }
    }
}
