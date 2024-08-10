using AutoMapper;
using ECommerceElctronics.Api.CQRS.Commands;
using ECommerceElctronics.Api.CQRS.Commands.BrandFolder;
using ECommerceElctronics.Api.CQRS.Commands.CartFolder;
using ECommerceElctronics.Api.CQRS.Commands.UserFolder;
using ECommerceElctronics.Api.CQRS.Handlers.CartFolder;
using ECommerceElctronics.Api.CQRS.Queries;
using ECommerceElctronics.Api.CQRS.Queries.BrandFolder;
using ECommerceElctronics.Api.CQRS.Queries.CartFolder;
using ECommerceElctronics.Api.CQRS.Queries.UserFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Requests;
using ECommerceElctronics.Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceElctronics.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CartsController : BasesController
    {
        public CartsController(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator) : base(unitOfWork, mapper, mediator)
        {
        }

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

        //[HttpPut]   
        //public async Task<IActionResult> UpdateBrand([FromBody] Brand brand)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    var command = new UpdateBrandCommand(brand);

        //    var result = await _mediator.Send(command);

        //    if (result == false)
        //        return BadRequest();

        //    return NoContent();
        //}
        //[HttpDelete("{brandId}")]
        //public async Task<IActionResult> DeleteBrand(int brandId)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    var command = new DeleteBrandCommand(brandId);

        //    var result = await _mediator.Send(command);

        //    if (result == false)
        //        return BadRequest();

        //    return NoContent();
        //}
    }
}
