using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.OrderFolder
{
    public record ChangeOrderStatusCommand(int OrderId, OrderStatus Status) : IRequest<bool>;
}
