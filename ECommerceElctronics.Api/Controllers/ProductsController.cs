using AutoMapper;
using ECommerceElctronics.Api.CQRS.Commands;
using ECommerceElctronics.Api.CQRS.Commands.BrandFolder;
using ECommerceElctronics.Api.CQRS.Commands.ProductFolder;
using ECommerceElctronics.Api.CQRS.Queries;
using ECommerceElctronics.Api.CQRS.Queries.BrandFolder;
using ECommerceElctronics.Api.CQRS.Queries.ProductFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Requests;
using ECommerceElctronics.Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceElctronics.Api.Controllers
{
    public class ProductsController : BasesController
    {
        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator) : base(unitOfWork, mapper, mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var query = new GetAllProductsQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetByIdProduct(int productId)
        {
            var query = new GetByIdProductQuery(productId);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] CreateProductRequest brand)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new CreateProductCommand(brand);

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromForm] UpdateProductRequest productRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new UpdateProductCommand(productId, productRequest);

            var result = await _mediator.Send(command);

            if (result == false)
                return BadRequest();

            return NoContent();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new DeleteProductCommand(productId);

            var result = await _mediator.Send(command);

            if (result == false)
                return BadRequest();

            return NoContent();
        }
    }
}
