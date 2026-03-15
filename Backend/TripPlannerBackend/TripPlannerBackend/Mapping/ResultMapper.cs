using Microsoft.AspNetCore.Mvc;
using TripPlannerBackend.Models.Domains;

namespace TripPlannerBackend.Mapping
{
    public static class ResultMapper
    {
        public static IActionResult ToActionResult<T>(
        this Result<T> result,
        ControllerBase controller)
        {
            return result switch
            {
                Success<T> s => controller.Ok(s.Value),
                Error<T> e => controller.BadRequest(e.Message),
                _ => controller.StatusCode(500)
            };
        }
    }
}
