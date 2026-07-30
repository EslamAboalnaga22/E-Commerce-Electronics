using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.ProductFolder
{
    public record DeleteProductCommand(int ProductId) : IRequest<bool>;
}
