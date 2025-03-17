using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurantById
{
    public class UpdateRestaurantByIdCommandHandler(
        ILogger<UpdateRestaurantByIdCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IMapper mapper
    ) : IRequestHandler<UpdateRestaurantByIdCommand, bool>
    {
        public async Task<bool> Handle(UpdateRestaurantByIdCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Checking if restaurant with id {@id} exists", request.Id);
            Restaurant? restaurant = await restaurantsRepository.GetById(request.Id);

            if (restaurant is null)
                return false;

            logger.LogInformation("Updating restaurant with id {@id}", request.Id);
            mapper.Map(request, restaurant);
            await restaurantsRepository.SaveChangesAsync();
            return true;
        }
    }
}