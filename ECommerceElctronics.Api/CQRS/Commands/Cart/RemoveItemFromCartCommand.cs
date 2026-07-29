using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.Cart
{
    public record RemoveItemFromCartCommand(int CartItemId): IRequest<bool>;
}
