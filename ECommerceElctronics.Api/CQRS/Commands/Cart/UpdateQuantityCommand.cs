using ECommerceElctronics.Entities.Dtos.Requests;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.Cart
{
    public record UpdateQuantityCommand(UpdateQuantityRequest QuantityRequest) : IRequest<bool>;
}
