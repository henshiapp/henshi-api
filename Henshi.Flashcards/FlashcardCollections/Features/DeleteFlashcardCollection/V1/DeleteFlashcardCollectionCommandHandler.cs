using Henshi.Flashcards.FlashcardCollections.Repositories;


namespace Henshi.Flashcards.FlashcardCollections.Features.DeleteFlashcardCollection.V1;

class DeleteFlashcardCollectionCommandHandler(IFlashcardCollectionRepository _flashcardCollectionRepository)
{
    public async Task Handle(DeleteFlashcardCollectionCommand query)
    {
        var collection = await _flashcardCollectionRepository.GetByIdAsync(query.Id);

        if (collection is null)
        {
            return;
        }

        _flashcardCollectionRepository.Delete(collection);
        await _flashcardCollectionRepository.SaveChangesAsync();
    }
}
