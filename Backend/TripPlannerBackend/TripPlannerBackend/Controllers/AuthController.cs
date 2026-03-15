using Microsoft.AspNetCore.Mvc;
using TripPlannerBackend.Extensions;
using TripPlannerBackend.Mapping;
using TripPlannerBackend.Models.Domains;
using TripPlannerBackend.Models.DTOs;
using TripPlannerBackend.Services.Interfaces;

namespace TripPlannerBackend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            return result.ToActionResult(this);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            var result = await _authService.AuthenticateAsync(request);
            return result.ToActionResult(this);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = Request.Cookies["refresh_token"];

            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized();
            }

            var result = await _authService.RefreshAsync(refreshToken);

            return result.ToActionResult(this);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["refresh_token"];

            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized();

            var result = await _authService.LogoutAsync(refreshToken);
            return result.ToLogoutActionResult(this);
        }
    }
}
