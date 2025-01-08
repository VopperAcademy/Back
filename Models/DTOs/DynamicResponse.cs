using System.Net;

namespace vopperAcademyBackEnd.Models.DTOs;

public class DynamicResponse<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public int StatusCode { get; set; }
    
    public static DynamicResponse<T> CreateSuccess(T data, int statusCode = 200)
    {
        return new DynamicResponse<T>
        {
            Data = data,
            Success = true,
            StatusCode = statusCode,
            ErrorMessage = null
        };
    }

    public static DynamicResponse<T> CreateError(string errorMessage, int statusCode = 500)
    {
        return new DynamicResponse<T>
        {
            Data = default,
            Success = false,
            StatusCode = statusCode,
            ErrorMessage = errorMessage
        };
    }
}