using System;
using Henshi.Flashcards.Presentation.Dtos;

namespace Henshi.Flashcards.Application.Services;

public interface IFlashcardStatsService
{
    Task<FlashcardStatsResponse> GetStats(string userId);
}
