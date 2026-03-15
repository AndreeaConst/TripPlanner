namespace TripPlannerBackend.Common.Errors
{
    public static class AuthErrors
    {
        public const string EmailExists = "auth.email_exists";
        public const string UserNotFound = "auth.user_not_found";
        public const string InvalidPassword = "auth.invalid_password";
        public const string RefreshTokenInvalid = "auth.refresh_token_invalid";
    }
}
