using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}