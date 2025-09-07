using System.Data;
using Henshi.Flashcards.Domain.Models;
using Henshi.Flashcards.Presentation.Dtos;
using Henshi.Shared.Presentation.Dtos;

namespace Henshi.Flashcards.Application.Services;

public interface IFlashcardService
{
    Task Create(string question, string answer, Guid collectionId, string userId);
    Task<Guid?> Delete(Guid id, string userId);
    Task<List<Flashcard>> ListAvailableForRecall(Guid collectionId, string userId);
    Task<(List<Flashcard>, PaginationMetadata)> List(Guid collectionId, string? search, int page, int pageSize, string userId);
    Task SaveRecall(Guid collectionId, List<RecallFlashcardsResult> answers, string userId);
}