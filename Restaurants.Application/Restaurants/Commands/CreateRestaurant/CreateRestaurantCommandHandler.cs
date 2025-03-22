using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler(
        IRestaurantsRepository restaurantsRepository,
        ILogger<CreateRestaurantCommandHandler> logger,
        IMapper mapper,
        IUserContext userContext
    ) : IRequestHandler<CreateRestaurantCommand, Restaurant>
    {
        public async Task<Restaurant> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();
            logger.LogInformation("{userEmail} [{UserId}] is creating restaurant {@createRestaurantDTO}",
                currentUser!.Email, currentUser.Id, request);

            var restaurant = mapper.Map<Restaurant>(request);
            restaurant.OwnerId = currentUser.Id;

            var restaurantCreated = await restaurantsRepository.Create(restaurant);
            return restaurantCreated;
        }
    }
}