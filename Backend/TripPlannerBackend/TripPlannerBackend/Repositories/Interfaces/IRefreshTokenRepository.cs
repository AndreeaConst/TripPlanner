using System.Threading.Tasks;
using TripPlannerBackend.Models.Domains;

namespace TripPlannerBackend.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task AddAsync(RefreshToken token);
        Task SaveChangesAsync();
        Task RevokeAsync(RefreshToken token);
    }
}