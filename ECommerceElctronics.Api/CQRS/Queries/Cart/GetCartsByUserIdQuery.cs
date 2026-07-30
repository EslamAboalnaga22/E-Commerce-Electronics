using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Queries.CartFolder
{
    public record GetCartsByUserIdQuery(int UserId) :IRequest<CartDtoResponse>;

}
