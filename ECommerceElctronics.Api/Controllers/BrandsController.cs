using AutoMapper;
using ECommerceElctronics.Api.CQRS.Commands;
using ECommerceElctronics.Api.CQRS.Commands.BrandFolder;
using ECommerceElctronics.Api.CQRS.Queries;
using ECommerceElctronics.Api.CQRS.Queries.BrandFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Requests;
using ECommerceElctronics.Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceElctronics.Api.Controllers
{
    public class BrandsController : BasesController
    {
        public BrandsController(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator) : base(unitOfWork, mapper, mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            var query = new GetAllBrandsQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{brandId}")]
        public async Task<IActionResult> GetByIdBrand(int brandId)
        {
            var query = new GetByIdBrandQuery(brandId);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddBrand([FromBody] CreateBrandRequest brand)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new CreateBrandCommand(brand);

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBrand([FromBody] Brand brand)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new UpdateBrandCommand(brand);

            var result = await _mediator.Send(command);

            if (result == false)
                return BadRequest();

            return NoContent();
        }
        [HttpDelete("{brandId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBrand(int brandId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var command = new DeleteBrandCommand(brandId);

            var result = await _mediator.Send(command);

            if (result == false)
                return BadRequest();

            return NoContent();
        }
    }
}
