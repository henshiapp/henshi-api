using Henshi.Flashcards.Domain.Models;
using Henshi.Shared.Presentation.Dtos;

namespace Henshi.Flashcards.Application.Services;

public interface IFlashcardCollectionService
{
    Task Create(string title, string? description, string icon, string userId);
    Task<Guid?> Delete(Guid id, string userId);
    Task<(List<FlashcardCollection>, PaginationMetadata)> List(string? search, int page, int pageSize, string userId);
}