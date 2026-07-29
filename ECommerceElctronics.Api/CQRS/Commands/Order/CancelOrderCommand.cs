using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.OrderFolder
{
    public record CancelOrderCommand(int OrderId) : IRequest<bool>;
}
