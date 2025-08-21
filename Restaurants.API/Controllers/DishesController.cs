﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDish;
using Restaurants.Application.Dishes.Commands.Queries;
using Restaurants.Application.Dishes.Commands.UpdateDish;
using Restaurants.Application.Dishes.DishesDtos;

namespace Restaurants.API.Controllers
{
    [Route("api/restaurants/{restaurantId}/dishes")]
    [ApiController]
    [Authorize]
    public class DishesController(IMediator mediator) : Controller
    {

        [HttpPatch]
        public async Task<ActionResult> UpadateDish([FromRoute] int restaurantId, UpdateDishByIdForRestaurantCommand command)
        {
            command.RestaurantId = restaurantId;
            var dishId = await mediator.Send(command);
           
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;
            var dishId = await mediator.Send(command);
            return CreatedAtAction(nameof(GetByIdForRestaurant), new { restaurantId, dishId}, null);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant([FromRoute] int restaurantId)
        {
            var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));
            return Ok(dishes);
        }

        [HttpGet("{dishId}")]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetByIdForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var dishes = await mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));
            return Ok(dishes);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllDishesFromRestaurant([FromRoute] int restaurantId)
        {
            await mediator.Send(new DeleteAllDishFromRestaurantCommand(restaurantId));
            return Ok();
        }

        [HttpDelete("{dishId}")]
        public async Task<IActionResult> DeleteDishFromRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            await mediator.Send(new DeleteDishFromRestaurantCommand(restaurantId, dishId));
            return Ok();
        }
    }
}
