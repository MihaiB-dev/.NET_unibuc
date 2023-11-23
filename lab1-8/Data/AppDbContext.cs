using lab1_8.Models;
using Microsoft.EntityFrameworkCore;

namespace lab1_8.Data
{
    public class AppDbContext : ApplicationDbContext
    {
        public AppDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
