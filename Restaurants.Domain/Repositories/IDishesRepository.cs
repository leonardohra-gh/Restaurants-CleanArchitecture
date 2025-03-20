using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
    public interface IDishesRepository
    {
        Task<Dish> Create(Dish dish);
        Task<Dish?> Get(int id);
        Task<IEnumerable<Dish>> GetAllFromRestaurant(int restaurantId);
    }
}