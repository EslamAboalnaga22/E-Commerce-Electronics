using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Dtos.Requests;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Commands.CartFolder
{
    public record AddItemToCartCommand(AddItemCartRequest AddItemCart) : IRequest<Result<bool>>;
}
