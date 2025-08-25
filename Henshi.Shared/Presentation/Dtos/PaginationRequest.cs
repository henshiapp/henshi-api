using System;

namespace Henshi.Flashcards.Presentation.Dtos;

public record PaginationRequest(int Page = 1, int PageSize = 10);
