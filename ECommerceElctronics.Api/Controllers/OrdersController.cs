using AutoMapper;
using ECommerceElctronics.DataServices.CQRS.Commands.OrderFolder;
using ECommerceElctronics.DataServices.CQRS.Queries.OrderFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Requests;
using ECommerceElctronics.Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceElctronics.Api.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class OrdersController(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator) : BasesController(unitOfWork, mapper, mediator)
    {
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var query = new GetAllOrdersQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetOrdersByCartId(int Id)
        {
            var query = new GetOrderByIdQuery(Id);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("OrderUesr/{userId}")]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        {
            var query = new GetUserOrdersQuery(userId);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPut("ChangeStatues/{orderId:int}")]
        public async Task<IActionResult> ChangeStatuesOrder(int orderId, OrderStatus Status)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new ChangeOrderStatusCommand(orderId, Status);

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut("Cancel/{orderId:int}")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new CancelOrderCommand(orderId);

            var result = await _mediator.Send(command);

            return Ok(result);
        }

 

        [HttpPut("{orderId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrder(int orderId, [FromForm] UpdateOrderRequest orderRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new UpdateOrderCommand(orderId, orderRequest);

            var result = await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{orderId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new DeleteOrderCommand(orderId);

            var result = await _mediator.Send(command);

            return NoContent();
        }
    }
}
