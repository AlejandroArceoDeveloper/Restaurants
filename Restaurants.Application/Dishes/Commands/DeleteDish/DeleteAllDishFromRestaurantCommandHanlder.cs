using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDish
{
    public class DeleteAllDishFromRestaurantCommandHanlder(ILogger<DeleteAllDishFromRestaurantCommandHanlder> logger,
        IMapper mapper, IRestaurantRepository restaurantRepository, IDishesRepository dishesRepository) : IRequestHandler<DeleteAllDishFromRestaurantCommand>
    {
        public async Task Handle(DeleteAllDishFromRestaurantCommand request, CancellationToken cancellationToken)
        {
           logger.LogWarning("Deleting all dishes for restaurant with ID {RestaurantId}", request.RestaurantId);
           
            var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }
            if (restaurant.Dishes == null || !restaurant.Dishes.Any())
            {
                throw new NotFoundException(nameof(Dish), "No dishes found for this restaurant.");
            }
            await dishesRepository.DeleteAll(restaurant.Dishes);
        }
    }
}
