using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }

    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public CurrentUser? GetCurrentUser()
        {
            var user = httpContextAccessor.HttpContext?.User;
            if (user == null)
            {
                throw new InvalidOperationException("User is not present.");
            }
            if (user.Identity == null || !user.Identity.IsAuthenticated)
            {
                throw new InvalidOperationException("User is not authenticated.");
            }
            var userId = user.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            var email = user.FindFirst(x => x.Type == ClaimTypes.Email)?.Value ?? string.Empty;
            var roles = user.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);

            return new CurrentUser(userId, email, roles);
        }
    }
}
