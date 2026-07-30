using ECommerceElctronics.DataServices.ResultPattern;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Commands.OrderFolder
{
    public record CancelOrderCommand(int OrderId) : IRequest<Result<bool>>;
}
