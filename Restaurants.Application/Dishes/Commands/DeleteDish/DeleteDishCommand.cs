using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteDish
{
    public class DeleteDishCommand(int restaurantId, int dishId) : IRequest
    {
        public int RestaurantId { get; set; } = restaurantId;
        public int DishId { get; set; } = dishId;
    }
}