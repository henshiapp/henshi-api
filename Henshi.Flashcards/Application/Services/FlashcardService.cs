using System;
using System.Runtime.CompilerServices;
using Henshi.Flashcards.Domain.Models;
using Henshi.Flashcards.Infraestructure;
using Henshi.Flashcards.Infraestructure.Repositories;
using Henshi.Flashcards.Presentation.Dtos;
using Henshi.Shared.Presentation.Dtos;

namespace Henshi.Flashcards.Application.Services;

public class FlashcardService : IFlashcardService
{
    private readonly IFlashcardRepository _flashcardRepository;

    public FlashcardService(IFlashcardRepository flashcardRepository)
    {
        _flashcardRepository = flashcardRepository;
    }

    public async Task Create(string question, string answer, Guid cardCollectionId)
    {
        await _flashcardRepository.AddAsync(Flashcard.Create(question, answer, cardCollectionId));
        await _flashcardRepository.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        await _flashcardRepository.DeleteAsync(id);
        await _flashcardRepository.SaveChangesAsync();
    }

    public async Task<(List<Flashcard>, PaginationMetadata)> List(string? search, int page, int pageSize)
    {
        return await _flashcardRepository.ListAsync(search, page, pageSize);
    }

    public async Task<List<Flashcard>> ListAvailableForRecall(Guid collectionId)
    {
        return await _flashcardRepository.ListAllAvailableForRecallAsync(collectionId);
    }

    public async Task SaveRecall(Guid collectionId, List<RecallFlashcardsResult> answers)
    {
        var flashcards = await _flashcardRepository.ListByCollectionId(collectionId);

        foreach (var answer in answers)
        {
            var flashcard = flashcards.Where(f => f.Id == answer.FlashcardId).FirstOrDefault();

            if (flashcard is null) continue;

            if (answer.Correct)
            {
                flashcard.AdvanceToNextGrade();
            }
            else
            {
                flashcard.ReturnToLastGrade();
            }
        }

        await _flashcardRepository.SaveChangesAsync();
    }
}
