using AutoMapper;
using ECommerceElctronics.Api.CQRS.Commands;
using ECommerceElctronics.Api.CQRS.Commands.BrandFolder;
using ECommerceElctronics.Api.CQRS.Commands.CategoryFolder;
using ECommerceElctronics.Api.CQRS.Queries;
using ECommerceElctronics.Api.CQRS.Queries.BrandFolder;
using ECommerceElctronics.Api.CQRS.Queries.CategoryFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Requests;
using ECommerceElctronics.Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceElctronics.Api.Controllers
{
    public class CategoriesController : BasesController
    {
        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator) : base(unitOfWork, mapper, mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var query = new GetAllCategoriesQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetByIdCategory(int categoryId)
        {
            var query = new GetByIdCategoryQuery(categoryId);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryRequest category)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new CreateCategoryCommand(category);

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new UpdateCategoryCommand(category);

            var result = await _mediator.Send(command);

            if (result == false)
                return BadRequest();

            return NoContent();
        }
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new DeleteCategoryCommand(categoryId);

            var result = await _mediator.Send(command);

            if (result == false)
                return BadRequest();

            return NoContent();
        }
    }
}
