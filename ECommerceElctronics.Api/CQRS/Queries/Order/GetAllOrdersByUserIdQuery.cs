using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Queries.OrderFolder
{
    public class GetAllOrdersByUserIdQuery : IRequest<IEnumerable<GetOrderDetailssResponse>>
    {
        public int UserId { get; set; }

        public GetAllOrdersByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}
