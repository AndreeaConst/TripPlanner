using TripPlannerBackend.Models.Domains;
using TripPlannerBackend.Models.DTOs;

namespace TripPlannerBackend.Services.Interfaces
{
    public interface IJwtService
    {
        string CreateAccessToken(UserAccount user);
        RefreshToken CreateRefreshToken();
    }
}