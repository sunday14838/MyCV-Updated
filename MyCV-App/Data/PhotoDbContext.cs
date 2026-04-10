using Microsoft.EntityFrameworkCore;
using MyCV_App.Models;

namespace MyCV_App.Data
{
    public class PhotoDbContext : DbContext
    {
        public PhotoDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Photo> Photos { get; set; }

    }
}
