using AutoMapper;
using ECommerceElctronics.Api.CQRS.Commands.Cart;
using ECommerceElctronics.Api.CQRS.Commands.CartFolder;
using ECommerceElctronics.Api.CQRS.Queries.CartFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceElctronics.Api.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class CartsController(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator) : BasesController(unitOfWork, mapper, mediator)
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCarts()
        {
            var query = new GetAllCartsQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{cartId:int}")]
        public async Task<IActionResult> GetCartsByCartId(int cartId)
        {
            var query = new GetCartsByCartIdQuery(cartId);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("UserCart/{userId:int}")]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> GetCartsByUserId(int userId)
        {
            var query = new GetCartsByUserIdQuery(userId);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("Ceckout")]
        public async Task<IActionResult> Ceckout(int UesrId)
        {
            var command = new CheckoutCommand(UesrId);

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("AddItemToCart")]
        [AllowAnonymous]
        public async Task<IActionResult> AddItemToCart(AddItemCartRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new AddItemToCartCommand(request);

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut("UpdateQuantity")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateQuantityInCart(UpdateQuantityRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new UpdateQuantityCommand(request);

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete("RemoveItem/{cartItemId:int}")]
        public async Task<IActionResult> RemoveItemFromCart(int cartItemId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new RemoveItemFromCartCommand(cartItemId);

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete("ClaerCart/{UesrId:int}")]
        public async Task<IActionResult> ClaerCart(int UesrId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new ClearCartCommand(UesrId);

            var result = await _mediator.Send(command);

            return Ok(result);
        } 
    }
}
