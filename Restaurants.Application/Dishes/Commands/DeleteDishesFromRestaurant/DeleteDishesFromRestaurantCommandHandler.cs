using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDishesFromRestaurant
{
    public class DeleteDishesFromRestaurantCommandHandler(
        ILogger<DeleteDishesFromRestaurantCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IDishesRepository dishesRepository
    ) : IRequestHandler<DeleteDishesFromRestaurantCommand>
    {
        public async Task Handle(DeleteDishesFromRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting all dishes from restaurant with id {id}", request.RestaurantId);

            var restaurant = await restaurantsRepository.GetById(request.RestaurantId) ??
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            var dishesFromRestaurant = await dishesRepository.GetAllFromRestaurant(request.RestaurantId);

            await dishesRepository.DeleteList(dishesFromRestaurant);
        }
    }
}