using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TripPlannerBackend.Data;
using TripPlannerBackend.Models.Domains;
using TripPlannerBackend.Repositories.Interfaces;

namespace TripPlannerBackend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<UserAccount> GetByEmailAsync(string email)
        {
            return await _db.UserAccount
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserAccount> GetByIdAsync(Guid id)
        {
            return await _db.UserAccount.FindAsync(id);
        }

        public Task AddAsync(UserAccount user)
        {
            _db.UserAccount.Add(user);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync()
            => _db.SaveChangesAsync();
    }
}