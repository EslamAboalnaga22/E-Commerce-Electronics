using ECommerceElctronics.DataServices.ResultPattern;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Commands.OrderFolder
{
    public record StatusProcessingOrderCommand(int OrderId) : IRequest<Result<bool>>;
}
