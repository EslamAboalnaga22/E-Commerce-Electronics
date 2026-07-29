using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.Cart
{
    public record ClearCartCommand(int UserId) : IRequest<bool>;
}
