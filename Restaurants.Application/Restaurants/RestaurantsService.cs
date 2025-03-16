using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants
{
    internal class RestaurantsService(IRestaurantsRepository restaurantsRepository,
        ILogger<RestaurantsService> logger,
        IMapper mapper) : IRestaurantsService
    {
        public async Task<IEnumerable<RestaurantDTO>> GetAllRestaurants()
        {
            logger.LogInformation("Getting all restaurants");
            var restaurants = await restaurantsRepository.GetAllAsync();
            var restaurantsDTO = mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
            return restaurantsDTO!;
        }

        public async Task<RestaurantDTO?> GetRestaurantById(int id)
        {
            logger.LogInformation("Getting restaurant with id {id}", id);
            var restaurant = await restaurantsRepository.GetById(id);
            var restaurantDTO = mapper.Map<RestaurantDTO?>(restaurant);
            return restaurantDTO;
        }
    }
}