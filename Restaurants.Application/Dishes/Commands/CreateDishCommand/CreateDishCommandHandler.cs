using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDishCommand
{
    public class CreateDishCommandHandler(
        ILogger<CreateDishCommand> logger,
        IRestaurantsRepository restaurantsRepository,
        IDishesRepository dishRepository,
        IMapper mapper
    ) : IRequestHandler<CreateDishCommand, Dish>
    {
        public async Task<Dish> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating new dish {@DishRequest} request", request);

            Restaurant? restaurant = await restaurantsRepository.GetById(request.RestaurantId) ??
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var dish = mapper.Map<Dish>(request);
            await dishRepository.Create(dish);
            return dish;
        }
    }
}