using TripPlannerBackend.Common.Errors;

namespace TripPlannerBackend.Common.Responses
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }

        public ApiError? Error { get; set; }

        public static ApiResponse<T> Success(T data)
        {
            return new ApiResponse<T> { Data = data };
        }

        public static ApiResponse<T> Failure(ApiError error)
        {
            return new ApiResponse<T> { Error = error };
        }
    }
}
