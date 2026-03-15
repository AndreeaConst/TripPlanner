namespace TripPlannerBackend.Common.Errors
{
    public class ApiError
    {
        public string Code { get; set; } = null!;
        //Summary message (to be displayed on top of the React Page as a summary or only message)
        public string? Message { get; set; } = null!;
        //List of all form errors, if any
        public Dictionary<string, string>? Errors { get; set; }
    }
}
