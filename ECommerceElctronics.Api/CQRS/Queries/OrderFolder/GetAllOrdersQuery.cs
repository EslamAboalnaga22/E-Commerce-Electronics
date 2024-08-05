using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Queries.OrderFolder
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<GetOrderDetailssResponse>>
    {

    }
}
