using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.DTOs
{
    public class DishDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public int? KiloCalories { get; set; }

        public static DishDTO FromEntity(Dish dish)
        {
            var dishDTO = new DishDTO()
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                KiloCalories = dish.KiloCalories
            };

            return dishDTO;
        }
    }
}