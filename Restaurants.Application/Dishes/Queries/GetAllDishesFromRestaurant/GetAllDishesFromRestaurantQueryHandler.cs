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

namespace Restaurants.Application.Dishes.Queries.GetAllDishesFromRestaurant
{
    public class GetAllDishesFromRestaurantQueryHandler(
        ILogger<GetAllDishesFromRestaurantQueryHandler> logger,
        IRestaurantsRepository repository,
        IMapper mapper
    ) : IRequestHandler<GetAllDishesFromRestaurantQuery, IEnumerable<DishDTO>>
    {
        public async Task<IEnumerable<DishDTO>> Handle(GetAllDishesFromRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting dishes from restaurant with id {@id}", request.RestaurantId);
            var restaurant = await repository.GetById(request.RestaurantId) ??
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var dishes = mapper.Map<IEnumerable<DishDTO>>(restaurant.Dishes);

            return dishes.Select(mapper.Map<DishDTO>);
        }
    }
}