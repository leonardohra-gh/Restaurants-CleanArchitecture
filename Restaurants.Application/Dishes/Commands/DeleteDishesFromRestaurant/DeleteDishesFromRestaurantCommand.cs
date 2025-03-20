using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteDishesFromRestaurant
{
    public class DeleteDishesFromRestaurantCommand(int restaurantId) : IRequest
    {
        public int RestaurantId { get; set; } = restaurantId;
    }
}