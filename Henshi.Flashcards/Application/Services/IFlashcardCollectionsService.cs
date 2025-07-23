using Henshi.Flashcards.Domain.Models;

namespace Henshi.Flashcards;

public interface IFlashcardCollectionsService
{
    Task Create(string Title, string? Description, string Icon);
    Task<List<FlashcardCollection>> List();
}