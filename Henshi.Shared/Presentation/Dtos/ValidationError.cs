namespace Henshi.Shared.Presentation.Dtos;

public class ValidationError
{
    public required string Field { get; set; }
    public required string Message { get; set; }
}
