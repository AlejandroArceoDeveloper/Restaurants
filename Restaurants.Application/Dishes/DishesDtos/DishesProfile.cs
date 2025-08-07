using AutoMapper;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.DishesDtos
{
    public class DishesProfile : Profile
    {
        public DishesProfile()
        {
            CreateMap<Dish, DishDto>();
        }
    }
}
