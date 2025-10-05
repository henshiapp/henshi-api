namespace Henshi.Shared.Dtos;

public class PaginationMetadata
{
    public required int Page { get; set; }
    public required int Offset { get; set; }
    public required int Size { get; set; }
    public required int TotalElements { get; set; }
    public required int TotalPages { get; set; }
}
