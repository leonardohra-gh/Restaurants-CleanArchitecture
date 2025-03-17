using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurantById
{
    public class DeleteRestaurantByIdCommandHandler(
        IRestaurantsRepository restaurantsRepository,
        ILogger<DeleteRestaurantByIdCommandHandler> logger
    ) : IRequestHandler<DeleteRestaurantByIdCommand, bool>
    {
        public async Task<bool> Handle(DeleteRestaurantByIdCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Checking if restaurant with id {id} exists", request.Id);
            Restaurant? restaurant = await restaurantsRepository.GetById(request.Id);

            if (restaurant is null)
                return false;

            await restaurantsRepository.Delete(restaurant);
            return true;
        }
    }
}