using FSRS.Core.Enums;
using FSRS.Core.Models;
using FSRS.Core.Services;
using Henshi.Flashcards.Domain.Models;
using Henshi.Flashcards.Domain.Repositories;
using Henshi.Flashcards.Domain.ValueObjects;
using Henshi.Flashcards.Presentation.Dtos;
using Henshi.Shared.Presentation.Dtos;

namespace Henshi.Flashcards.Application.Services;

public class FlashcardService(IFlashcardRepository flashcardRepository) : IFlashcardService
{
    private readonly IFlashcardRepository _flashcardRepository = flashcardRepository;

    public async Task Create(string question, string answer, Guid cardCollectionId, string userId)
    {
        await _flashcardRepository.AddAsync(Flashcard.Create(question, answer, cardCollectionId));
        await _flashcardRepository.SaveChangesAsync();
    }

    public async Task<Guid?> Delete(Guid id, string userId)
    {
        var flashcard = await _flashcardRepository.GetByIdAsync(id, userId);

        if (flashcard is null)
        {
            return null;
        }

        _flashcardRepository.Delete(flashcard);
        await _flashcardRepository.SaveChangesAsync();

        return id;
    }

    public async Task<(List<Flashcard>, PaginationMetadata)> List(Guid collectionId, string? search, int page, int pageSize, string userId)
    {
        return await _flashcardRepository.ListAsync(collectionId, search, userId, page, pageSize);
    }

    public async Task<List<Flashcard>> ListAvailableForRecall(Guid collectionId, string userId)
    {
        return await _flashcardRepository.ListAllAvailableForRecallAsync(collectionId, userId);
    }

    public async Task SaveRecall(Guid collectionId, List<RecallFlashcardsResult> answers, string userId)
    {
        var flashcards = await _flashcardRepository.ListByCollectionId(collectionId, userId);

        var scheduler = new Scheduler();

        foreach (var answer in answers)
        {
            var flashcard = flashcards.Where(f => f.Id == answer.FlashcardId).FirstOrDefault();

            if (flashcard is null) continue;

            var card = new Card(flashcard.Id, StateMapper.ToFsrs(flashcard.State), flashcard.Step, flashcard.Stability, flashcard.Difficulty, flashcard.NextRecall, flashcard.LastRecall);

            var (updatedCard, reviewLog) = scheduler.ReviewCard(card, GradeMapper.ToFsrs(answer.Grade));

            flashcard.SaveRecallInformation(
                updatedCard.Due,
                GradeMapper.FromFsrs(reviewLog.Rating),
                StateMapper.FromFsrs(updatedCard.State),
                updatedCard.Step,
                updatedCard.Stability,
                updatedCard.Difficulty
            );
        }

        await _flashcardRepository.SaveChangesAsync();
    }
}
