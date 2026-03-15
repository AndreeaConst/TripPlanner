using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TripPlannerBackend.Models.Domains;
using TripPlannerBackend.Models.DTOs;

namespace TripPlannerBackend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Result<UserAccountDto?>> RegisterAsync(RegisterUserRequest request);
        Task<Result<UserAccountDto>> AuthenticateAsync(LoginUserRequest request);
        Task<Result<UserAccountDto>> RefreshAsync(string refreshToken);
        Task<Result<bool>> LogoutAsync(string refreshToken);
    }
}
