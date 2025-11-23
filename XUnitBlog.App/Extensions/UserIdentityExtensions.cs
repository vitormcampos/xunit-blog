using System.Security.Claims;
using XUnitBlog.Domain.Entities;

namespace XUnitBlog.App.Extensions;

public static class UserIdentityExtensions
{
    public static bool RoleIsAdmin(this ClaimsPrincipal user)
    {
        return user.IsInRole(Role.ADMIN.ToString());
    }

    public static bool RoleIsEditor(this ClaimsPrincipal user)
    {
        return user.IsInRole(Role.EDITOR.ToString());
    }

    public static long GetId(this ClaimsPrincipal user)
    {
        var userIdString = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)!.Value;

        if (!long.TryParse(userIdString, out var userId))
        {
            throw new ArgumentException("Id de usuário invalido");
        }

        return userId;
    }

    public static string GetRole(this ClaimsPrincipal user)
    {
        var userRole = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value;

        return userRole;
    }
}
