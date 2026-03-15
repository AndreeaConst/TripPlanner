using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TripPlannerBackend.Data;
using TripPlannerBackend.Models.Domains;
using TripPlannerBackend.Repositories.Interfaces;

namespace TripPlannerBackend.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _db;

        public RefreshTokenRepository(AppDbContext db)
        {
            _db = db;
        }

        public Task<RefreshToken?> GetByTokenAsync(string token)
            => _db.RefreshToken.Include(t => t.User).SingleOrDefaultAsync(t => t.Token == token);

        public Task AddAsync(RefreshToken token)
        {
            _db.RefreshToken.Add(token);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync()
            => _db.SaveChangesAsync();

        public Task RevokeAsync(RefreshToken token)
        {
            token.Revoked = System.DateTime.UtcNow;
            return Task.CompletedTask;
        }
    }
}