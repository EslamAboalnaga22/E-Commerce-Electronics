using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Dtos.Requests;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Commands.BrandFolder
{
    public record CreateBrandCommand(CreateBrandRequest BrandRequest) : IRequest<Result<Brand>>;
}
