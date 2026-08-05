using ECommerceElctronics.DataServices.ResultPattern;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Commands.OrderFolder
{
    public record StatusRefunedOrderCommand(int OrderId) : IRequest<Result<bool>>;
}
