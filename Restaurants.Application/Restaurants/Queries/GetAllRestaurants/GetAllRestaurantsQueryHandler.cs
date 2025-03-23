using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryHandler(
        IRestaurantsRepository restaurantsRepository,
        ILogger<GetAllRestaurantsQueryHandler> logger,
        IMapper mapper
    ) : IRequestHandler<GetAllRestaurantsQuery, PagedResult<RestaurantDTO>>
    {
        public async Task<PagedResult<RestaurantDTO>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all restaurants");
            var (restaurants, totalCount) = await restaurantsRepository.GetAllMatchignAsync(
                request.SearchPhrase,
                request.PageSize,
                request.PageNumber
            );
            var restaurantsDTO = mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
            var pagedRestaurants = new PagedResult<RestaurantDTO>(restaurantsDTO, totalCount, request.PageSize, request.PageNumber);
            return pagedRestaurants;
        }
    }
}