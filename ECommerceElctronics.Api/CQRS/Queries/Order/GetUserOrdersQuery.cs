using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Queries.OrderFolder
{
    public record GetUserOrdersQuery(int UserId) : IRequest<IEnumerable<OrderDtoResponse>>;
}
