using System;
using System.Runtime.CompilerServices;
using Henshi.Flashcards.Domain.Models;
using Henshi.Flashcards.Infraestructure.Repositories;
using Henshi.Flashcards.Presentation.Dtos;
using Henshi.Shared.Presentation.Dtos;

namespace Henshi.Flashcards.Application.Services;

public class FlashcardCollectionService : IFlashcardCollectionService
{
    private readonly IFlashcardCollectionRepository _flashcardCollectionRepository;

    public FlashcardCollectionService(IFlashcardCollectionRepository flashcardCollectionRepository)
    {
        _flashcardCollectionRepository = flashcardCollectionRepository;
    }

    public async Task Create(string title, string? description, string icon, string userId)
    {
        await _flashcardCollectionRepository.AddAsync(new FlashcardCollection(title, description, icon, userId));
        await _flashcardCollectionRepository.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        await _flashcardCollectionRepository.DeleteAsync(id);
        await _flashcardCollectionRepository.SaveChangesAsync();
    }

    public async Task<(List<FlashcardCollection>, PaginationMetadata)> List(string userId, string? search, string search1, int page, int pageSize)
    {
        return await _flashcardCollectionRepository.ListAsync(userId, search, page, pageSize);
    }
}
