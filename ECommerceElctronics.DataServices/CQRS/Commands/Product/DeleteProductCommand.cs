using ECommerceElctronics.DataServices.ResultPattern;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Commands.ProductFolder
{
    public record DeleteProductCommand(int ProductId) : IRequest<Result<bool>>;
}
