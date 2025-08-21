using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Seeders
{
    internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
    {
        public async Task SeedAsync()
        {
            if (await dbContext.Database.CanConnectAsync())
            {

                if (!dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    dbContext.Restaurants.AddRange(restaurants);
                    await dbContext.SaveChangesAsync();
                }
                if (!dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    dbContext.Roles.AddRange(roles);
                    await dbContext.SaveChangesAsync();
                }
            }
          
        }

        private IEnumerable<IdentityRole> GetRoles()
        {
            return new List<IdentityRole>
            {
                new IdentityRole { Name = UserRoles.Admin, NormalizedName = UserRoles.Admin.ToUpper() },
                new IdentityRole { Name = UserRoles.Owner, NormalizedName = UserRoles.Owner.ToUpper() },
                new IdentityRole { Name = UserRoles.User, NormalizedName = UserRoles.User.ToUpper() }
            };
          
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            return
            [
                new Restaurant
        {
            Name = "La Parrilla Argentina",
            Description = "Especialidad en carnes a la brasa y cocina tradicional argentina.",
            Category = "Carnes",
            HasDelivery = true,
            ContactEmail = "contacto@laparrilla.com",
            ContactNumber = "123456789",
            Address = new Address
            {
                City = "Madrid",
                Street = "Calle Mayor 10",
                PostalCode = "28013"
            },
            Dishes = new List<Dish>
            {
                new Dish
                {
                    Name = "Bife de chorizo",
                    Description = "Corte de carne argentino a la parrilla.",
                    Price = 18.50m,
                    KiloCalories = 850
                },
                new Dish
                {
                    Name = "Empanada criolla",
                    Description = "Empanada rellena de carne y especias.",
                    Price = 3.50m,
                    KiloCalories = 250
                }
            }
        },
        new Restaurant
        {
            Name = "Sushi Sakura",
            Description = "Restaurante japonés con sushi fresco y platos tradicionales.",
            Category = "Japonés",
            HasDelivery = false,
            ContactEmail = "info@sushisakura.com",
            ContactNumber = "987654321",
            Address = new Address
            {
                City = "Barcelona",
                Street = "Avenida Diagonal 200",
                PostalCode = "08018"
            },
            Dishes = new List<Dish>
            {
                new Dish
                {
                    Name = "Sashimi variado",
                    Description = "Selección de pescado fresco cortado al estilo japonés.",
                    Price = 15.00m,
                    KiloCalories = 400
                },
                new Dish
                {
                    Name = "Maki de salmón",
                    Description = "Roll de arroz y salmón envuelto en alga nori.",
                    Price = 8.00m,
                    KiloCalories = 300
                }
            }
        }
            ];
        }
    }
}
