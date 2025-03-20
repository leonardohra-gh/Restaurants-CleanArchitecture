using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDish;
using Restaurants.Application.Dishes.Commands.DeleteDishesFromRestaurant;
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
            return CreatedAtAction(nameof(GetDish), new { restaurantId, dish.Id });
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

        [HttpDelete]
        public async Task<IActionResult> DeleteDishesFromRestaurant([FromRoute] int restaurantId)
        {
            await mediator.Send(new DeleteDishesFromRestaurantCommand(restaurantId));
            return NoContent();
        }

        [HttpDelete("{dishId}")]
        public async Task<IActionResult> DeleteDish([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            await mediator.Send(new DeleteDishCommand(restaurantId, dishId));
            return NoContent();
        }
    }
}