using Henshi.Flashcards.FlashcardCollections.Models;
using Henshi.Flashcards.FlashcardCollections.Repositories;

namespace Henshi.Flashcards.FlashcardCollections.Features.CreateFlashcardCollection.V1;

public class CreateFlashcardCollectionCommandHandler(IFlashcardCollectionRepository _flashcardCollectionRepository)
{
    public async Task Handle(CreateFlashcardCollectionCommand command)
    {
        var collection = new FlashcardCollection(
            command.Title,
            command.Description,
            command.Icon,
            command.UserId
        );

        await _flashcardCollectionRepository.AddAsync(collection);
        await _flashcardCollectionRepository.SaveChangesAsync();
    }
}
