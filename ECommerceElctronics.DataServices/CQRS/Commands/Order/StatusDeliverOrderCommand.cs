using ECommerceElctronics.DataServices.ResultPattern;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Commands.OrderFolder
{
    public record StatusDeliverOrderCommand(int OrderId) : IRequest<Result<bool>>;
}
