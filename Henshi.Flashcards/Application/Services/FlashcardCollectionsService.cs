using Henshi.Flashcards.Domain.Models;
using Henshi.Flashcards.Domain.Repositories;
using Henshi.Shared.Presentation.Dtos;

namespace Henshi.Flashcards.Application.Services;

public class FlashcardCollectionService(
    IFlashcardCollectionRepository flashcardCollectionRepository,
    IFlashcardRepository flashcardRepository
) : IFlashcardCollectionService
{
    private readonly IFlashcardCollectionRepository _flashcardCollectionRepository = flashcardCollectionRepository;
    private readonly IFlashcardRepository _flashcardRepository = flashcardRepository;

    public async Task Create(string title, string? description, string icon, string userId)
    {
        await _flashcardCollectionRepository.AddAsync(new FlashcardCollection(title, description, icon, userId));
        await _flashcardCollectionRepository.SaveChangesAsync();
    }

    public async Task<Guid?> Delete(Guid id, string userId)
    {
        var collection = await _flashcardCollectionRepository.GetByIdAsync(id, userId);

        if (collection is null) return null;

        _flashcardCollectionRepository.Delete(collection);

        foreach (var flashcard in collection.Flashcards)
        {
            _flashcardRepository.Delete(flashcard);
        }

        await _flashcardCollectionRepository.SaveChangesAsync();

        return id;
    }

    public async Task<(List<FlashcardCollection>, PaginationMetadata)> List(string? search, int page, int pageSize, string userId)
    {
        return await _flashcardCollectionRepository.ListAsync(search, userId, page, pageSize);
    }
}
