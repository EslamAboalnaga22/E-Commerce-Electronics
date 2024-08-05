using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Queries.OrderFolder
{
    public class GetAllOrdersByCartIdQuery : IRequest<IEnumerable<GetOrderDetailssResponse>>
    {
        public int CartId { get; set; }

        public GetAllOrdersByCartIdQuery(int cartId)
        {
            CartId = cartId;
        }
    }
}
