using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Repositories
{
    public interface IDishesRepository
    {
        Task<IEnumerable<Dish>> GetAllAsync();
        Task<Dish?> GetByIdAsync(int id);
        Task<int> Create(Dish entity);
        Task Delete(Dish entity);
        Task DeleteAll(IEnumerable<Dish> entities);
        Task SaveChanges();
    }
}
