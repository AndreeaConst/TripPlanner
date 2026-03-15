using Microsoft.AspNetCore.Mvc;
using TripPlannerBackend.Common.Errors;
using TripPlannerBackend.Common.Responses;
using TripPlannerBackend.Extensions;
using TripPlannerBackend.Models.Domains;
using TripPlannerBackend.Models.DTOs;


namespace TripPlannerBackend.Mapping
{
    public static class AuthResultMapper
    {
        public static IActionResult ToActionResult(
            this Result<UserAccountDto> result,
            ControllerBase controller)
        {
            if (result is Success<UserAccountDto> success)
            {
                controller.Response.SetAuthCookies(
                    success.Value.AccessToken,
                    success.Value.RefreshToken
                );

                return controller.Ok(new
                {
                    data = success.Value
                });
            }

            if (result is Error<UserAccountDto> error)
            {
                var apiError = new ApiError
                {
                    Code = error.Code,
                    Message = error.Message
                };

                return error.Code switch
                {
                    AuthErrors.EmailExists =>
                        controller.BadRequest(
                            ApiResponse<object>.Failure(
                                new ApiError
                                {
                                    Code = error.Code,
                                    Errors = new Dictionary<string, string>
                                    { 
                                        {"email", error.Message } 
                                    }
                                })),

                    AuthErrors.InvalidPassword =>
                        controller.BadRequest(
                            ApiResponse<object>.Failure(
                                new ApiError
                                {
                                    Code = error.Code,
                                    Errors = new Dictionary<string, string>
                                    {
                                        {"password", error.Message }
                                    }
                                })),

                    _ =>
                        controller.BadRequest(
                            ApiResponse<object>.Failure(apiError))
                };
            }


            return controller.StatusCode(500);
        }

        public static IActionResult ToLogoutActionResult(
           this Result<bool> result,
           ControllerBase controller)
        {
            if (result is Success<bool>)
            {
                controller.Response.ClearAuthCookies();

                return controller.Ok(new
                {
                    data = "logged_out"
                });
            }

            if (result is Error<bool> error)
            {
                return controller.Unauthorized(new
                {
                    error = new
                    {
                        code = error.Code,
                        message = error.Message
                    }
                });
            }

            return controller.StatusCode(500);
        }
    }
}
