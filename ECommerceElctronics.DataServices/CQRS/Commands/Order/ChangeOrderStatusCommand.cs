using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Commands.OrderFolder
{
    public record ChangeOrderStatusCommand(int OrderId, OrderStatus Status) : IRequest<Result<bool>>;
}
