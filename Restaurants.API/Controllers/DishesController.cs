using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Application.Dishes.Queries.GetAllDishesFromRestaurant;
using Restaurants.Application.Dishes.Queries.GetDish;

namespace Restaurants.API.Controllers
{
    [Route("api/restaurants/{restaurantId}/dishes")]
    [ApiController]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, [FromBody] CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;
            var dish = await mediator.Send(command);
            return CreatedAtAction(nameof(GetDish), dish.Id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDTO>>> GetDishesFromRestaurant([FromRoute] int restaurantId)
        {
            var dishes = await mediator.Send(new GetAllDishesFromRestaurantQuery(restaurantId));
            return Ok(dishes);
        }

        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishDTO>> GetDish([FromRoute] int dishId)
        {
            var dish = await mediator.Send(new GetDishCommand(dishId));
            return Ok(dish);
        }
    }
}