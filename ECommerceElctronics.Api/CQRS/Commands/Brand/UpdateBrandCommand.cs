using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.BrandFolder
{
    public record UpdateBrandCommand(Brand BrandRequest) : IRequest<bool>;
}
