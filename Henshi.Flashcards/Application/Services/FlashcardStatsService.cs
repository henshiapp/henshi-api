using Henshi.Flashcards.Domain.Repositories;
using Henshi.Flashcards.Presentation.Dtos;

namespace Henshi.Flashcards.Application.Services;

public class FlashcardStatsService(
    IFlashcardRepository flashcardRepository,
    IFlashcardCollectionRepository flashcardCollectionRepository
) : IFlashcardStatsService
{
    private readonly IFlashcardRepository _flashcardRepository = flashcardRepository;
    private readonly IFlashcardCollectionRepository _flashcardCollectionRepository = flashcardCollectionRepository;

    public async Task<FlashcardStatsResponse> GetStats(string userId)
    {
        return new FlashcardStatsResponse(
            FlashcardCollectionCount: await _flashcardCollectionRepository.GetCount(userId),
            FlashcardCount: await _flashcardRepository.GetCount(userId)
        );
    }
}
