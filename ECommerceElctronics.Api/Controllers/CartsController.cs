using AutoMapper;
using ECommerceElctronics.Api.CQRS.Commands.CartFolder;
using ECommerceElctronics.Api.CQRS.Queries.CartFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceElctronics.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CartsController(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator) : BasesController(unitOfWork, mapper, mediator)
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCarts()
        {
            var query = new GetAllCartsQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCartsByCartId(int cartId)
        {
            var query = new GetCartsByCartIdQuery(cartId);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("CartUser/{userId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetCartsByUserId(int userId)
        {
            var query = new GetCartsByUserIdQuery(userId);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddCart( CreateCartRequest cart)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new CreateCartCommand(cart);

            var result = await _mediator.Send(command);

            return Ok(result);
        }

    }
}
