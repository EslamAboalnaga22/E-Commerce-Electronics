using AutoMapper;
using ECommerceElctronics.Api.CQRS.Commands;
using ECommerceElctronics.Api.CQRS.Commands.BrandFolder;
using ECommerceElctronics.Api.CQRS.Commands.UserFolder;
using ECommerceElctronics.Api.CQRS.Queries;
using ECommerceElctronics.Api.CQRS.Queries.BrandFolder;
using ECommerceElctronics.Api.CQRS.Queries.UserFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Requests;
using ECommerceElctronics.Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceElctronics.Api.Controllers
{
    public class UsersController : BasesController
    {
        public UsersController(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator) : base(unitOfWork, mapper, mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var query = new GetAllUsersQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        //[HttpGet("{brandId}")]
        //public async Task<IActionResult> GetByIdBrand(int brandId)
        //{
        //    var query = new GetByIdBrandQuery(brandId);

        //    var result = await _mediator.Send(query);

        //    return Ok(result);
        //}

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new CreateUserCommand(user);

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
