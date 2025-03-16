using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants
{
    public interface IRestaurantsService
    {
        Task<IEnumerable<RestaurantDTO>> GetAllRestaurants();
        Task<RestaurantDTO?> GetRestaurantById(int id);
    }
}