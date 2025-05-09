using System.Security.Claims;

namespace oMeli_Back.Utils;

public static class ClaimsPrincipalExtensions
{
    public static Guid? GetUserId(this ClaimsPrincipal user)
    {
        var claim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(claim))
            return null;

        return Guid.TryParse(claim, out var userId) ? userId : null;
    }
}
