using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Commands.BrandFolder
{
    public record UpdateBrandCommand(Brand BrandRequest) : IRequest<Result<bool>>;
}
