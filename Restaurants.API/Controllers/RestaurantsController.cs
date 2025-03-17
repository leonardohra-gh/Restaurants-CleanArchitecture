using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurantById;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurantById;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
            return restaurant is null ? NotFound() : Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand createRestaurantCommand)
        {
            var restaurantCreated = await mediator.Send(createRestaurantCommand);
            return CreatedAtAction(nameof(GetById), new { restaurantCreated.Id }, null);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById([FromRoute] int id)
        {
            var isDeleted = await mediator.Send(new DeleteRestaurantByIdCommand(id));
            return isDeleted ? NoContent() : NotFound();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, [FromBody] UpdateRestaurantByIdCommand updateRestaurantByIdCommand)
        {
            updateRestaurantByIdCommand.Id = id;
            var isModified = await mediator.Send(updateRestaurantByIdCommand);
            return isModified ? NoContent() : NotFound();
        }
    }
}