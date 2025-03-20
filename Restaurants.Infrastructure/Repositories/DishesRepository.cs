using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistance;

namespace Restaurants.Infrastructure.Repositories
{
    internal class DishesRepository(RestaurantDbContext dbContext) : IDishesRepository
    {
        public async Task<Dish> Create(Dish dish)
        {
            dbContext.Dishes.Add(dish);
            await dbContext.SaveChangesAsync();
            return dish;
        }

        public async Task<Dish?> Get(int id)
        {
            var dish = await dbContext.Dishes
                .FirstOrDefaultAsync(d => d.Id == id);
            return dish;
        }

        public async Task<IEnumerable<Dish>> GetAllFromRestaurant(int restaurantId)
        {
            var dishes = await dbContext.Dishes
                .Where(d => d.RestaurantId == restaurantId)
                .ToListAsync();

            return dishes;
        }
    }
}