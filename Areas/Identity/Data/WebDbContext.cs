using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppCheck.Areas.Identity.Data;
using WebAppCheck.Models;

namespace WebAppCheck.Data;

public class WebDbContext : IdentityDbContext<WebAppUser>
{
    public WebDbContext(DbContextOptions<WebDbContext> options)
        : base(options)
    {

    }
    public DbSet<Post> Posts { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
