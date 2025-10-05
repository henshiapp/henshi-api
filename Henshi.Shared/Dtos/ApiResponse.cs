namespace Henshi.Shared.Dtos;

public class ApiResponse<T>
{
    public required string Status { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public PaginationMetadata? Metadata { get; set; }
    public ValidationError[]? Errors { get; set; }

    public static ApiResponse<T> Success(T data, PaginationMetadata? metadata = null)
    {
        return new ApiResponse<T>
        {
            Status = "success",
            Data = data,
            Metadata = metadata
        };
    }

    public static ApiResponse<T> Success(string message)
    {
        return new ApiResponse<T>
        {
            Status = "success",
            Message = message,
        };
    }

    public static ApiResponse<T> Error(ValidationError[] errors, string message)
    {
        return new ApiResponse<T>
        {
            Status = "error",
            Message = message,
            Errors = errors
        };
    }
}
