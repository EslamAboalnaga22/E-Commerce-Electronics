using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Queries.CartFolder
{
    public record GetCartsByCartIdQuery(int CartId) : IRequest<CartDtoResponse>;
}
