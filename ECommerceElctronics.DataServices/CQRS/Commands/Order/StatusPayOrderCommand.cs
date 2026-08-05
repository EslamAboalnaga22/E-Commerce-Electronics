using ECommerceElctronics.DataServices.ResultPattern;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Commands.OrderFolder
{
    public record StatusPayOrderCommand(int OrderId) : IRequest<Result<bool>>;
}
