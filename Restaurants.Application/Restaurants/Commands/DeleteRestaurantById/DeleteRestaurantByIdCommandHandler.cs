using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurantById
{
    public class DeleteRestaurantByIdCommandHandler(
        IRestaurantsRepository restaurantsRepository,
        ILogger<DeleteRestaurantByIdCommandHandler> logger
    ) : IRequestHandler<DeleteRestaurantByIdCommand>
    {
        public async Task Handle(DeleteRestaurantByIdCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Checking if restaurant with id {@id} exists", request.Id);
            Restaurant? restaurant = await restaurantsRepository.GetById(request.Id) ??
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            logger.LogInformation("Deleting restaurant with id {@id}", request.Id);
            await restaurantsRepository.Delete(restaurant);
        }
    }
}