using System.Data;
using Henshi.Flashcards.Domain.Models;
using Henshi.Flashcards.Presentation.Dtos;
using Henshi.Shared.Presentation.Dtos;

namespace Henshi.Flashcards.Application.Services;

public interface IFlashcardCollectionService
{
    Task Create(string Title, string? Description, string Icon);
    Task Delete(Guid id);
    Task<(List<FlashcardCollection>, PaginationMetadata)> List(string? search, int page, int pageSize);
}