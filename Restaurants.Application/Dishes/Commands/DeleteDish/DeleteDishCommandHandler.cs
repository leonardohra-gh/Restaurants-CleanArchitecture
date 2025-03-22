using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDish
{
    public class DeleteDishCommandHandler(
        ILogger<DeleteDishCommand> logger,
        IRestaurantsRepository restaurantsRepository,
        IDishesRepository dishesRepository,
        IRestaurantAuthorizationService authorizationService
    ) : IRequestHandler<DeleteDishCommand>
    {
        public async Task Handle(DeleteDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting dish with id {dishId} from restaurant with id {restaurantId}",
                request.DishId, request.RestaurantId);

            var restaurant = await restaurantsRepository.GetById(request.RestaurantId) ??
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            var dish = await dishesRepository.Get(request.DishId) ??
                throw new NotFoundException(nameof(Dish), request.DishId.ToString());

            if (!authorizationService.Authorize(restaurant, Domain.Constants.ResourceOperation.Update))
                throw new ForbidException();

            await dishesRepository.Delete(dish);
        }
    }
}