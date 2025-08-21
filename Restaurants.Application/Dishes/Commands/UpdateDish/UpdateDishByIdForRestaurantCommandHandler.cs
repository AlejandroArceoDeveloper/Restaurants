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

namespace Restaurants.Application.Dishes.Commands.UpdateDish
{
    public class UpdateDishByIdForRestaurantCommandHandler(ILogger<UpdateDishByIdForRestaurantCommandHandler> logger, 
        IMapper mapper, IRestaurantRepository restaurantRepository, 
        IDishesRepository dishesRepository ): IRequestHandler<UpdateDishByIdForRestaurantCommand, int>
    {
        public async Task<int> Handle(UpdateDishByIdForRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling UpdateDishByIdForRestaurantCommand for dish: {@DishRequest}", request.Name);
            var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                logger.LogWarning("Restaurant with ID {RestaurantId} not found", request.RestaurantId);
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }
            var dish = await dishesRepository.GetByIdAsync(request.DishId);
            if (dish == null)
            {
                logger.LogWarning("Dish with ID {DishId} not found", request.DishId);
                throw new NotFoundException(nameof(Dish), request.DishId.ToString());
            }
           
            mapper.Map(request, dish);

            await dishesRepository.SaveChanges();
            return dish.Id;
        }
    }
}
