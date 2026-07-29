using ECommerceElctronics.Entities.Dtos.Requests;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.BrandFolder
{
    public record CreateBrandCommand(CreateBrandRequest BrandRequest) : IRequest<Brand>;
}
