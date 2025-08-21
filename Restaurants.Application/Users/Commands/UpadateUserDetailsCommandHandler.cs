using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Commands.UpdateDish;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users.Commands
{
    public class UpadateUserDetailsCommandHandler(ILogger<UpdateDishByIdForRestaurantCommandHandler> logger,
        IUserContext userContext, IUserStore<User> userStore) : IRequestHandler<UpdateUserDetailsCommand>
    {
        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            logger.LogInformation("Handling UpdateUserDetailsCommand for user: {@UserRequest} with id {@userId}", request, user!.Id);
            var dbUser = await userStore.FindByIdAsync(user.Id, cancellationToken);

            if (dbUser == null)
            {
                logger.LogWarning("User with id {@userId} not found", user.Id);
                throw new NotFoundException(nameof(User), user.Id);
            }

            dbUser.Nationality = request.Nationality;
            dbUser.DateOfBirth = request.DateOfBirth;

            await userStore.UpdateAsync(dbUser, cancellationToken);
            

        }
    }
}
