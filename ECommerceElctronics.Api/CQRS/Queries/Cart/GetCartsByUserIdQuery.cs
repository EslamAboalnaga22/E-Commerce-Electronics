using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Queries.CartFolder
{
    public class GetCartsByUserIdQuery : IRequest<IEnumerable<CartDtoResponse>>
    {
        public int UserId { get; set; }

        public GetCartsByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}
