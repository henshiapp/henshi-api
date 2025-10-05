using Henshi.Flashcards.FlashcardCollections.Models;
using Henshi.Flashcards.FlashcardCollections.Repositories;


namespace Henshi.Flashcards.FlashcardCollections.Features.UpdateFlashcardCollection.V1;

class UpdateFlashcardCollectionCommandHandler(IFlashcardCollectionRepository _flashcardCollectionRepository)
{
    public async Task<FlashcardCollection?> Handle(UpdateFlashcardCollectionCommand command)
    {
        var collection = await _flashcardCollectionRepository.GetByIdAsync(command.Id);

        if (collection is null) return null;

        collection.Title = command.Title;
        collection.Description = command.Description;
        collection.Icon = command.Icon;

        await _flashcardCollectionRepository.SaveChangesAsync();

        return collection;
    }
}
