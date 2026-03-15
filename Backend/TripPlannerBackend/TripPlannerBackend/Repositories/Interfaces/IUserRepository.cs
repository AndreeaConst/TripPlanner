using System.Threading.Tasks;
using TripPlannerBackend.Models.Domains;
using TripPlannerBackend.Models.DTOs;

namespace TripPlannerBackend.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserAccount> GetByIdAsync(Guid id);
        Task<UserAccount> GetByEmailAsync(string email);
        Task AddAsync(UserAccount user);
        Task SaveChangesAsync();
    }
}