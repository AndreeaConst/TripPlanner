using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using TripPlannerBackend.Common.Errors;
using TripPlannerBackend.Models.Domains;
using TripPlannerBackend.Models.DTOs;
using TripPlannerBackend.Repositories.Interfaces;
using TripPlannerBackend.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TripPlannerBackend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtService _jwtService;
        private readonly PasswordHasher<UserAccount> _passwordHasher;
        private readonly IMapper _mapper;  

        public AuthService(
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IJwtService jwtService,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtService = jwtService;
            _passwordHasher = new PasswordHasher<UserAccount>();
            _mapper = mapper;
        }

        public async Task<Result<UserAccountDto?>> RegisterAsync(RegisterUserRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if(user is not null)
            {
                return new Error<UserAccountDto?>(AuthErrors.EmailExists, "Email already exists");
            }
            if (user is null)
            {
                // Hash password BEFORE saving
                var newUser = new UserAccount();
                newUser.Password = _passwordHasher.HashPassword(newUser, request.Password);
                newUser.Email = request.Email;
                newUser.Name = request.Name;

                await _userRepository.AddAsync(newUser);
                await _userRepository.SaveChangesAsync();

                // Generate tokens using the entity
                var accessToken = _jwtService.CreateAccessToken(newUser);
                var refreshToken = _jwtService.CreateRefreshToken();

                refreshToken.UserId = newUser.Id;
                await _refreshTokenRepository.AddAsync(refreshToken);
                await _refreshTokenRepository.SaveChangesAsync();

                // Map AFTER persistence
                var userDto = _mapper.Map<UserAccountDto>(newUser);
                userDto.AccessToken = accessToken;
                userDto.RefreshToken = refreshToken.Token;

                return new Success<UserAccountDto?>(userDto);
            }


            throw new InvalidOperationException("Unknown result type");
        }

        public async Task<Result<UserAccountDto>> AuthenticateAsync(LoginUserRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if(user is null)
            {
                return new Error<UserAccountDto>(AuthErrors.UserNotFound, "User not found");
            }

            if(user is not null)
            {
                var dehashedPassword = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);
                if (dehashedPassword == PasswordVerificationResult.Failed)
                    return new Error<UserAccountDto>(AuthErrors.InvalidPassword, "Password was not correct");

                var accessToken = _jwtService.CreateAccessToken(user);
                var refreshToken = _jwtService.CreateRefreshToken();

                refreshToken.UserId = user.Id;
                await _refreshTokenRepository.AddAsync(refreshToken);
                await _refreshTokenRepository.SaveChangesAsync();

                // Map AFTER persistence
                var userDto = _mapper.Map<UserAccountDto>(user);
                userDto.AccessToken = accessToken;
                userDto.RefreshToken = refreshToken.Token;

                return new Success<UserAccountDto>(userDto);
            }

            throw new InvalidOperationException("Unknown result type");
        }

        public async Task<Result<UserAccountDto>> RefreshAsync(string refreshToken)
        {
            var tokenRecord = await _refreshTokenRepository.GetByTokenAsync(refreshToken);

            if (tokenRecord == null || !tokenRecord.IsActive)
            {
                return new Error<UserAccountDto>(
                    AuthErrors.RefreshTokenInvalid,
                    "Invalid refresh token"
                );
            }

            var user = await _userRepository.GetByIdAsync(tokenRecord.UserId);

            if (user is null)
            {
                return new Error<UserAccountDto>(AuthErrors.UserNotFound, "User not found");
            }

            if (user is not null)
            {
                tokenRecord.Revoked = DateTime.UtcNow;

                var newRefreshToken = _jwtService.CreateRefreshToken();
                newRefreshToken.UserId = user.Id;
                tokenRecord.ReplacedByToken = newRefreshToken.Token;

                await _refreshTokenRepository.AddAsync(newRefreshToken);
                await _refreshTokenRepository.SaveChangesAsync();

                var newAccessToken = _jwtService.CreateAccessToken(user);

                var userDto = _mapper.Map<UserAccountDto>(user);
                userDto.AccessToken = newAccessToken;
                userDto.RefreshToken = newRefreshToken.Token;

                return new Success<UserAccountDto>(userDto);
            }

            throw new InvalidOperationException("Unknown result type");
        }

        public async Task<Result<bool>> LogoutAsync(string refreshToken)
        {
            var tokenRecord = await _refreshTokenRepository.GetByTokenAsync(refreshToken);

            if (tokenRecord == null)
            {
                return new Error<bool>(
                    AuthErrors.RefreshTokenInvalid,
                    "Invalid refresh token"
                );
            }

            await _refreshTokenRepository.RevokeAsync(tokenRecord);
            await _refreshTokenRepository.SaveChangesAsync();

            return new Success<bool>(true);
        }
    }
}