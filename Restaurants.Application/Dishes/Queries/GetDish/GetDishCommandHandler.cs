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

namespace Restaurants.Application.Dishes.Queries.GetDish
{
    public class GetDishCommandHandler(
        ILogger<GetDishCommandHandler> logger,
        IMapper mapper,
        IDishesRepository repository
    ) : IRequestHandler<GetDishCommand, DishDTO>
    {
        public async Task<DishDTO> Handle(GetDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting dish with Id {@id}", request.Id);

            var dish = await repository.Get(request.Id) ??
                throw new NotFoundException(nameof(Dish), request.Id.ToString());

            var dishDto = mapper.Map<DishDTO>(dish);

            return dishDto;
        }
    }
}