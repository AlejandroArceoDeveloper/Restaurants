using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Roles
{
    public class AddUserRoleCommandHandler(ILogger<AddUserRoleCommandHandler> logger,
        UserManager<User> userManager, RoleManager<IdentityRole> roleManager
        ) : IRequestHandler<AddUserRoleCommand>
    {
        public async Task Handle(AddUserRoleCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Adding role {@RoleName} to user {@UserEmail}", request.RoleName, request.UserEmail);
            var user = await userManager.FindByEmailAsync(request.UserEmail);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.UserEmail);
            }
            var role = await roleManager.FindByNameAsync(request.RoleName);
            if (role == null)
            {
                throw new NotFoundException(nameof(IdentityRole), request.RoleName);
            }
            logger.LogInformation("User {@UserEmail} found, adding role {@RoleName}", request.UserEmail, request.RoleName);
             await userManager.AddToRoleAsync(user, role.Name!);
        
        
        }
    }
}
