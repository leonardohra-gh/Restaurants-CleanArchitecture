using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Restaurants.Application.Dishes.DTOs;

namespace Restaurants.Application.Dishes.Queries.GetAllDishesFromRestaurant
{
    public class GetAllDishesFromRestaurantQuery(int restaurantId) : IRequest<IEnumerable<DishDTO>>
    {
        public int RestaurantId { get; set; } = restaurantId;
    }
}