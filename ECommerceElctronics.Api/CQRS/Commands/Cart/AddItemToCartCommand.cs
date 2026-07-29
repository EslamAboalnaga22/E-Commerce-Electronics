using ECommerceElctronics.Entities.Dtos.Requests;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.CartFolder
{
    public record AddItemToCartCommand(AddItemCartRequest AddItemCart) : IRequest<bool>;
}
