using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Queries.CartFolder
{
    public class GetCartsByCartIdQuery : IRequest<GetCartDetailsResponse>
    {
        public int CartId { get; set; }

        public GetCartsByCartIdQuery(int cartId)
        {
            CartId = cartId;
        }
    }
}
