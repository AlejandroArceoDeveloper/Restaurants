using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.DeleteDish
{
    public class DeleteDishFromRestaurantCommandHandler(ILogger<DeleteDishFromRestaurantCommandHandler> logger,
        IMapper mapper, IRestaurantRepository restaurantRepository, IDishesRepository dishesRepository) : IRequestHandler<DeleteDishFromRestaurantCommand>
    {
        public async Task Handle(DeleteDishFromRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogWarning("Deleting dish with ID {DishId} for restaurant with ID {RestaurantId}", request.DishId, request.RestaurantId);

            var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
        
            if (restaurant == null)
            {
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }

            var dish = restaurant.Dishes.FirstOrDefault(x => x.Id == request.DishId);
            if (dish == null)
            {
                throw new NotFoundException(nameof(Dish), request.DishId.ToString());
            }

            dishesRepository.Delete(dish);

        }
    }
}
