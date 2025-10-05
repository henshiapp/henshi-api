namespace Henshi.Flashcards.Flashcards.Features.SaveRecall.V1;

record SaveRecallCommand(Guid CollectionId, List<RecallFlashcardsResult> Answers);
