using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger
        ,IRestaurantRepository restaurantRepository,
        IDishesRepository dishesRepository,
        IMapper mapper) : IRequestHandler<CreateDishCommand, int>
    {

        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {

            logger.LogInformation("Handling CreateDishCommand for dish: {@DishRequest}", request.Name);
            var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                logger.LogWarning("Restaurant with ID {RestaurantId} not found", request.RestaurantId);
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }

            var dish = mapper.Map<Dish>(request);

            await dishesRepository.Create(dish);

            return dish.Id;
        }
    }


}
