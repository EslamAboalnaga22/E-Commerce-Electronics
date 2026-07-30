using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Dtos.Requests;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Commands.OrderFolder
{
    public record UpdateOrderCommand(int OrderIdRequest, UpdateOrderRequest OrderRequest) : IRequest<Result<bool>>;
}
