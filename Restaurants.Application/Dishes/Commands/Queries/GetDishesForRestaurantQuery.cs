using MediatR;
using Restaurants.Application.Dishes.DishesDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.Queries
{
    public class GetDishesForRestaurantQuery(int restaurantId):IRequest<IEnumerable<DishDto>>
    {
        public int RestaurantId { get; } = restaurantId;
    }
}
