using ECommerceElctronics.DataServices.ResultPattern;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Commands.Cart
{
    public record ClearCartCommand(int UserId) : IRequest<Result<bool>>;
}
