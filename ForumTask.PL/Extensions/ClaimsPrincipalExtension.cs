using System;
using System.Security.Claims;

namespace ForumTask.PL.Extensions {
    static class ClaimsPrincipalExtension {
        public static int GetId(this ClaimsPrincipal user)
            => Convert.ToInt32(user.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
