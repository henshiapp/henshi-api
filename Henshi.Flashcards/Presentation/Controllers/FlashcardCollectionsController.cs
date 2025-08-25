using Henshi.Flashcards.Application.Services;
using Henshi.Flashcards.Domain.Models;
using Henshi.Flashcards.Presentation.Dtos;
using Henshi.Shared.Presentation.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Henshi.Flashcards.Presentation.Controllers;

[ApiController]
[Route("/api/v1/card-collections")]
[Authorize]
public class FlashcardCollectionsController : Controller
{
    private readonly IFlashcardCollectionService _flashcardCollectionsService;
    private readonly IFlashcardService _flashcardService;

    public FlashcardCollectionsController(
        IFlashcardCollectionService flashcardCollectionsService,
        IFlashcardService flashcardService
    )
    {
        _flashcardCollectionsService = flashcardCollectionsService;
        _flashcardService = flashcardService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFlashcardCollectionRequest request)
    {
        await _flashcardCollectionsService.Create(
            request.Title,
            request.Description,
            request.Icon
        );

        return Ok(ApiResponse<Task>.Success("Card collection created successfully!"));
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] ListFlashcardCollectionRequest request)
    {
        var (list, metadata) = await _flashcardCollectionsService.List(request.Search, request.Page, request.PageSize);

        return Ok(
            ApiResponse<List<FlashcardCollection>>.Success(list, metadata)
        );
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _flashcardCollectionsService.Delete(id);
        return Ok(ApiResponse<List<FlashcardCollection>>.Success("Card collection deleted successfully!"));
    }

    [HttpPost("{id:guid}/flashcards")]
    public async Task<IActionResult> CreateFlashcard(Guid id, [FromBody] CreateFlashcardRequest request)
    {
        await _flashcardService.Create(
            request.Question,
            request.Answer,
            id
        );

        return Ok(ApiResponse<Task>.Success("Flashcard created successfully!"));
    }

    [HttpGet("{id:guid}/flashcards")]
    public async Task<IActionResult> ListFlashcards([FromQuery] ListFlashcardCollectionRequest request)
    {
        var (list, metadata) = await _flashcardService.List(request.Search, request.Page, request.PageSize);

        return Ok(
            ApiResponse<List<Flashcard>>.Success(list, metadata)
        );
    }

    [HttpDelete("{collectionId:guid}/flashcards/{id:guid}")]
    public async Task<IActionResult> DeleteFlashcard(Guid id)
    {
        await _flashcardService.Delete(id);
        return Ok(ApiResponse<Task>.Success("Flashcard deleted successfully!"));
    }

    [HttpGet("{id:guid}/flashcards/recall")]
    public async Task<IActionResult> GetRecall(Guid id)
    {
        var avaliableForRecall = await _flashcardService.ListAvailableForRecall(id);

        return Ok(ApiResponse<List<Flashcard>>.Success(avaliableForRecall, null));
    }

    [HttpPost("{id:guid}/flashcards/recall")]
    public async Task<IActionResult> SaveRecall(Guid id, SaveRecallFlashcardRequest request)
    {
        await _flashcardService.SaveRecall(id, request.Answers);
        return Ok(ApiResponse<Task>.Success("Recall saved successfully!"));
    }
}
