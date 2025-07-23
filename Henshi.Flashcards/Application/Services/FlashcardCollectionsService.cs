using System;
using Henshi.Flashcards.Domain.Models;
using Henshi.Flashcards.Infraestructure;

namespace Henshi.Flashcards.Application;

public class FlashcardCollectionsService : IFlashcardCollectionsService
{
    private readonly IFlashcardCollectionRepository _flashcardCollectionRepository;

    public FlashcardCollectionsService(IFlashcardCollectionRepository flashcardCollectionRepository)
    {
        _flashcardCollectionRepository = flashcardCollectionRepository;
    }

    public async Task Create(string title, string? description, string icon)
    {
        await _flashcardCollectionRepository.AddAsync(new FlashcardCollection(title, description, icon, "1"));
        await _flashcardCollectionRepository.SaveChangesAsync();
    }

    public async Task<List<FlashcardCollection>> List()
    {
        return await _flashcardCollectionRepository.ListAsync();
    }
}
