using FSRS.Core.Models;
using FSRS.Core.Services;
using Henshi.Flashcards.Domain.Repositories;
using Henshi.Flashcards.Domain.ValueObjects;


namespace Henshi.Flashcards.Flashcards.Features.SaveRecall.V1;

class SaveRecallCommandHandler(IFlashcardRepository _flashcardRepository)
{
    public async Task Handle(SaveRecallCommand command)
    {
        var flashcards = await _flashcardRepository.ListByCollectionId(command.CollectionId);

        var scheduler = new Scheduler();

        foreach (var answer in command.Answers)
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