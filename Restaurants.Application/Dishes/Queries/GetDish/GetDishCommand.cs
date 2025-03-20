using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Restaurants.Application.Dishes.DTOs;

namespace Restaurants.Application.Dishes.Queries.GetDish
{
    public class GetDishCommand(int dishId) : IRequest<DishDTO>
    {
        public int Id { get; set; } = dishId;

    }
}