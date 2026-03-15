namespace TripPlannerBackend.Models.Domains
{
    public abstract record Result<T>;

    public record Success<T>(T Value) : Result<T>;

    public record Error<T>(string Code, string Message) : Result<T>;
}
