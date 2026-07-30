using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Dtos.Requests;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Commands.ProductFolder
{
    public record UpdateProductCommand(int ProductIdRequest, UpdateProductRequest ProductRequest) : IRequest<Result<bool>>;
}
