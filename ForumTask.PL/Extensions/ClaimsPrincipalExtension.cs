using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ForumTask.PL.Extensions {
    static class ClaimsPrincipalExtension {
        public static int GetId(this ClaimsPrincipal user)
            => Convert.ToInt32(user.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
