using ECommerceElctronics.Entities.Dtos.Requests;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.ProductFolder
{
    public record UpdateProductCommand(int ProductIdRequest, UpdateProductRequest ProductRequest) : IRequest<bool>;
}
