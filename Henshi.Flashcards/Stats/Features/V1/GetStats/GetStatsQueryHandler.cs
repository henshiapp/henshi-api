using Henshi.Flashcards.Domain.Repositories;
using Henshi.Flashcards.FlashcardCollections.Repositories;


namespace Henshi.Flashcards.Stats.Features.V1.GetStats;

class GetStatsQueryHandler(
    IFlashcardRepository _flashcardRepository,
    IFlashcardCollectionRepository _flashcardCollectionRepository
)
{
    public async Task<StatsResponse> Handle()
    {
        return new StatsResponse(
            FlashcardCollectionCount: await _flashcardCollectionRepository.GetCount(),
            FlashcardCount: await _flashcardRepository.GetCount()
        );
    }
}
