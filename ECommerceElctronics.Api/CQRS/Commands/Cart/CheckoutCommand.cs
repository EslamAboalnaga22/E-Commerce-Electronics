using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.Cart
{
    public record CheckoutCommand(int UserId) : IRequest<int>;
}
