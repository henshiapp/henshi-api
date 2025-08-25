using System.Data;
using Henshi.Flashcards.Domain.Models;
using Henshi.Flashcards.Presentation.Dtos;
using Henshi.Shared.Presentation.Dtos;

namespace Henshi.Flashcards.Application.Services;

public interface IFlashcardService
{
    Task Create(string question, string answer, Guid collectionId);
    Task Delete(Guid id);
    Task<List<Flashcard>> ListAvailableForRecall(Guid collectionId);
    Task<(List<Flashcard>, PaginationMetadata)> List(string? search, int page, int pageSize);
    Task SaveRecall(Guid collectionId, List<RecallFlashcardsResult> answers);
}