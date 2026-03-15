using Microsoft.AspNetCore.Http;
using System;

namespace TripPlannerBackend.Extensions
{
    public static class HttpResponseExtensions
    {
        public static void SetAuthCookies(this HttpResponse response, string accessToken, string refreshToken)
        {
            if (response == null) throw new ArgumentNullException(nameof(response));

            var accessOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(10)
            };
            response.Cookies.Append("access_token", accessToken, accessOptions);

            var refreshOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            response.Cookies.Append("refresh_token", refreshToken, refreshOptions);
        }

        public static void ClearAuthCookies(this HttpResponse response)
        {
            response.Cookies.Delete("access_token");
            response.Cookies.Delete("refresh_token");
        }
    }
}