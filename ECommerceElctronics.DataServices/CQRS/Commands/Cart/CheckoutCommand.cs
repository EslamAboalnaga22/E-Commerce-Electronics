using ECommerceElctronics.DataServices.ResultPattern;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Commands.Cart
{
    public record CheckoutCommand(int UserId) : IRequest<Result<int>>;
}
