using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.DeleteDish
{
    public class DeleteDishFromRestaurantCommand: IRequest
    {
        public int RestaurantId { get; }
        public int DishId { get; }
        public DeleteDishFromRestaurantCommand(int restaurantId, int dishId)
        {
            RestaurantId = restaurantId;
            DishId = dishId;
        }
    }
   
}
