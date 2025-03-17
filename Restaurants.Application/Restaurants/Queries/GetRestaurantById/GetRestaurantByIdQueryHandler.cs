using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQueryHandler(
        IRestaurantsRepository restaurantsRepository,
        ILogger<GetRestaurantByIdQueryHandler> logger,
        IMapper mapper
    ) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDTO>
    {
        public async Task<RestaurantDTO> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting restaurant with id {@id}", request.Id);
            var restaurant = await restaurantsRepository.GetById(request.Id) ??
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString()); ;
            var restaurantDTO = mapper.Map<RestaurantDTO>(restaurant);
            return restaurantDTO;
        }
    }
}