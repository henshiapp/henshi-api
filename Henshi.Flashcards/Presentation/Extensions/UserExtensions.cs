using System.Security.Claims;

namespace Henshi.Flashcards.Presentation.Extensions;

public static class UserExtension
{
    public static string? Id(this ClaimsPrincipal user)
    {
        return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}