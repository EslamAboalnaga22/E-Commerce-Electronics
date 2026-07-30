using ECommerceElctronics.DataServices.ResultPattern;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Commands.OrderFolder
{
    public record DeleteOrderCommand(int OrderId) : IRequest<Result<bool>>;
}
