using System;
using System.Security.Claims;

namespace ForumTask.PL.Extensions
{
    internal static class ClaimsPrincipalExtension
    {
        public static int GetId(this ClaimsPrincipal user)
        {
            return Convert.ToInt32(user.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
