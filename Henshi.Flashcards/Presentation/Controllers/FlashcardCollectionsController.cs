using Microsoft.AspNetCore.Mvc;

namespace Henshi.Flashcards;

[ApiController]
[Route("/api/v1/flashcards/collections")]
public class FlashcardCollectionsController : Controller
{
    private readonly IFlashcardCollectionsService _flashcardCollectionsService;

    public FlashcardCollectionsController(IFlashcardCollectionsService flashcardCollectionsService)
    {
        _flashcardCollectionsService = flashcardCollectionsService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFlashcardCollectionRequest request)
    {
        await _flashcardCollectionsService.Create(
            request.Title,
            request.Description,
            request.Icon
        );

        return Created();
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        return Ok(await _flashcardCollectionsService.List());
    }

}
