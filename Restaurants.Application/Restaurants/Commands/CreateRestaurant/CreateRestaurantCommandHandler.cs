using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler(
        IRestaurantsRepository restaurantsRepository,
        ILogger<CreateRestaurantCommandHandler> logger,
        IMapper mapper) : IRequestHandler<CreateRestaurantCommand, Restaurant>
    {
        public async Task<Restaurant> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating restaurant {@createRestaurantDTO}", request);
            var restaurant = mapper.Map<Restaurant>(request);
            var restaurantCreated = await restaurantsRepository.Create(restaurant);
            return restaurantCreated;
        }
    }
}