using AutoMapper;
using ECommerceElctronics.Api.CQRS.Commands;
using ECommerceElctronics.Api.CQRS.Commands.BrandFolder;
using ECommerceElctronics.Api.CQRS.Commands.CartFolder;
using ECommerceElctronics.Api.CQRS.Commands.OrderFolder;
using ECommerceElctronics.Api.CQRS.Commands.ProductFolder;
using ECommerceElctronics.Api.CQRS.Commands.UserFolder;
using ECommerceElctronics.Api.CQRS.Handlers.CartFolder;
using ECommerceElctronics.Api.CQRS.Queries;
using ECommerceElctronics.Api.CQRS.Queries.BrandFolder;
using ECommerceElctronics.Api.CQRS.Queries.CartFolder;
using ECommerceElctronics.Api.CQRS.Queries.OrderFolder;
using ECommerceElctronics.Api.CQRS.Queries.UserFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Requests;
using ECommerceElctronics.Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceElctronics.Api.Controllers
{
    public class OrdersController : BasesController
    {
        public OrdersController(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator) : base(unitOfWork, mapper, mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var query = new GetAllOrdersQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("OrderUesr/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        {
            var query = new GetAllOrdersByUserIdQuery(userId);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("CartUesr/{cartId}")]

        public async Task<IActionResult> GetOrdersByCartId(int cartId)
        {
            var query = new GetAllOrdersByCartIdQuery(cartId);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        //[HttpPost]
        //public async Task<IActionResult> AddUCart( CreateCartRequest cart)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    var command = new CreateCartCommand(cart);

        //    var result = await _mediator.Send(command);

        //    return Ok(result);
        //}

        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(int orderId, [FromForm] UpdateOrderRequest orderRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new UpdateOrderCommand(orderId, orderRequest);

            var result = await _mediator.Send(command);

            if (result == false)
                return BadRequest();

            return NoContent();
        }
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new DeleteOrderCommand(orderId);

            var result = await _mediator.Send(command);

            if (result == false)
                return BadRequest();

            return NoContent();
        }
    }
}
