using System.Security.Claims;
using Henshi.Flashcards.Application.Services;
using Henshi.Flashcards.Domain.Models;
using Henshi.Flashcards.Presentation.Dtos;
using Henshi.Flashcards.Presentation.Extensions;
using Henshi.Shared.Presentation.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Henshi.Flashcards.Presentation.Controllers;

[ApiController]
[Route("/v1/card-collections")]
[Authorize]
public class FlashcardCollectionsController(
    IFlashcardCollectionService flashcardCollectionsService,
    IFlashcardService flashcardService,
    IFlashcardStatsService flashcardStatsService
) : Controller
{
    private readonly IFlashcardCollectionService _flashcardCollectionsService = flashcardCollectionsService;
    private readonly IFlashcardService _flashcardService = flashcardService;
    private readonly IFlashcardStatsService _flashcardStatsService = flashcardStatsService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFlashcardCollectionRequest request)
    {
        var userId = User.Id();

        if (userId is null) return Unauthorized();

        await _flashcardCollectionsService.Create(
            request.Title,
            request.Description,
            request.Icon,
            userId
        );

        return Ok(ApiResponse<Task>.Success("Card collection created successfully!"));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var userId = User.Id();

        if (userId is null) return Unauthorized();

        var collection = await _flashcardCollectionsService.GetById(id, userId);

        if (collection is null)
        {
            return NotFound(ApiResponse<Task>.Error([], "Collection not found"));
        }

        return Ok(ApiResponse<FlashcardCollection>.Success(collection));
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFlashcardCollectionRequest request)
    {
        var userId = User.Id();

        if (userId is null) return Unauthorized();

        var updatedCollection = await _flashcardCollectionsService.Update(
            id,
            request.Title,
            request.Description,
            request.Icon,
            userId
        );

        if (updatedCollection is null)
        {
            return NotFound(ApiResponse<Task>.Error([], "Collection not found"));
        }

        return Ok(ApiResponse<Task>.Success("Card collection updated successfully!"));
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] ListFlashcardCollectionRequest request)
    {
        var userId = User.Id();

        if (userId is null) return Unauthorized();

        var (list, metadata) = await _flashcardCollectionsService.List(request.Search, request.Page, request.PageSize, userId);

        return Ok(
            ApiResponse<List<FlashcardCollection>>.Success(list, metadata)
        );
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = User.Id();

        if (userId is null) return Unauthorized();

        var deleted = await _flashcardCollectionsService.Delete(id, userId);

        if (deleted is null) return NotFound();

        return Ok(ApiResponse<List<FlashcardCollection>>.Success("Card collection deleted successfully!"));
    }

    [HttpPost("{collectionId:guid}/flashcards")]
    public async Task<IActionResult> CreateFlashcard(Guid collectionId, [FromBody] CreateFlashcardRequest request)
    {
        var userId = User.Id();

        if (userId is null) return Unauthorized();

        await _flashcardService.Create(
            request.Question,
            request.Answer,
            collectionId,
            userId
        );

        return Ok(ApiResponse<Task>.Success("Flashcard created successfully!"));
    }

    [HttpPatch("{collectionId:guid}/flashcards/{id:guid}")]
    public async Task<IActionResult> UpdateFlashcard(Guid id, [FromBody] UpdateFlashcardRequest request)
    {
        var userId = User.Id();

        if (userId is null) return Unauthorized();

        var updatedFlashcard = await _flashcardService.Update(
            id,
            request.Question,
            request.Answer,
            userId
        );

        if (updatedFlashcard is null)
        {
            return NotFound(ApiResponse<Task>.Error([], "Flashcard not found"));
        }

        return Ok(ApiResponse<Task>.Success("Flashcard updated successfully!"));
    }

    [HttpGet("{collectionId:guid}/flashcards")]
    public async Task<IActionResult> ListFlashcards(Guid collectionId, [FromQuery] ListFlashcardCollectionRequest request)
    {
        var userId = User.Id();

        if (userId is null) return Unauthorized();

        var (list, metadata) = await _flashcardService.List(collectionId, request.Search, request.Page, request.PageSize, userId);

        return Ok(
            ApiResponse<List<Flashcard>>.Success(list, metadata)
        );
    }

    [HttpGet("{collectionId:guid}/flashcards/{id:guid}")]
    public async Task<IActionResult> GetFlashcardById(Guid id)
    {
        var userId = User.Id();

        if (userId is null) return Unauthorized();

        var flashcard = await _flashcardService.GetById(id, userId);

        if (flashcard is null)
        {
            return NotFound(ApiResponse<Task>.Error([], "Flashcard not found"));
        }

        return Ok(ApiResponse<Flashcard>.Success(flashcard));
    }

    [HttpDelete("{collectionId:guid}/flashcards/{id:guid}")]
    public async Task<IActionResult> DeleteFlashcard(Guid id)
    {
        var userId = User.Id();

        if (userId is null) return Unauthorized();

        var deleted = await _flashcardService.Delete(id, userId);

        if (deleted is null) return NotFound();

        return Ok(ApiResponse<Task>.Success("Flashcard deleted successfully!"));
    }

    [HttpGet("{collectionId:guid}/flashcards/recall")]
    public async Task<IActionResult> GetRecall(Guid collectionId)
    {
        var userId = User.Id();

        if (userId is null) return Unauthorized();

        var avaliableForRecall = await _flashcardService.ListAvailableForRecall(collectionId, userId);

        return Ok(ApiResponse<List<Flashcard>>.Success(avaliableForRecall, null));
    }

    [HttpPost("{collectionId:guid}/flashcards/recall")]
    public async Task<IActionResult> SaveRecall(Guid collectionId, SaveRecallFlashcardRequest request)
    {
        var userId = User.Id();

        if (userId is null) return Unauthorized();

        await _flashcardService.SaveRecall(collectionId, request.Answers, userId);
        return Ok(ApiResponse<Task>.Success("Recall saved successfully!"));
    }

    [HttpGet("stats")]
    public async Task<IActionResult> Stats()
    {
        var userId = User.Id();

        if (userId is null) return Unauthorized();

        return Ok(await _flashcardStatsService.GetStats(userId));
    }
}
