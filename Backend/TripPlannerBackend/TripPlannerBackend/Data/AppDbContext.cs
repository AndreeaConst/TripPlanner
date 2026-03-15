using Microsoft.EntityFrameworkCore;
using TripPlannerBackend.Models.Domains;

namespace TripPlannerBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserAccount> UserAccount => Set<UserAccount>();
        public DbSet<RefreshToken> RefreshToken => Set<RefreshToken>();
    }
}
