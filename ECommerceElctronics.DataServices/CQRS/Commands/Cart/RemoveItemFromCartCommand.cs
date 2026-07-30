using ECommerceElctronics.DataServices.ResultPattern;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Commands.Cart
{
    public record RemoveItemFromCartCommand(int CartItemId): IRequest<Result<bool>>;
}
