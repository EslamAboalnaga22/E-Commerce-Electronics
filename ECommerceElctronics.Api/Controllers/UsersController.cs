using AutoMapper;
using ECommerceElctronics.Api.CQRS.Commands.UserFolder;
using ECommerceElctronics.Api.CQRS.Queries.UserFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceElctronics.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator) : BasesController(unitOfWork, mapper, mediator)
    {
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var query = new GetAllUsersQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByIdUser(int userId)
        {
            var query = new GetByIdUserQuery(userId);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new CreateUserCommand(user);

            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
