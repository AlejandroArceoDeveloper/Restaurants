using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Dishs.Infrastructure.Repositories
{
    internal class DishesRepository(RestaurantsDbContext dbContext) : IDishesRepository
    {
        public async Task<int> Create(Dish entity)
        {
            dbContext.Dishes.Add(entity);
            await dbContext.SaveChangesAsync();
            
            return entity.Id;
        }

        public async Task DeleteAll(IEnumerable<Dish>dishes)
        {
            dbContext.Dishes.RemoveRange(dishes);
            await dbContext.SaveChangesAsync();

        }

        public async Task Delete(Dish entity)
        {
            dbContext.Dishes.Remove(entity);
            await dbContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<Dish>> GetAllAsync()
        {
            var Dishs = await dbContext.Dishes.ToListAsync();
            return Dishs;
        }

        public async Task<Dish> GetByIdAsync(int id)
        {
            var Dish = await dbContext.Dishes

                .FirstOrDefaultAsync(r => r.Id == id);
            return Dish;
        }

        public async Task SaveChanges()
        {
            dbContext.SaveChangesAsync();
        }
    }
}
