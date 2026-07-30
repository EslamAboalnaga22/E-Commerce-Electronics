using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Dtos.Requests;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Commands.Cart
{
    public record UpdateQuantityCommand(UpdateQuantityRequest QuantityRequest) : IRequest<Result<bool>>;
}
