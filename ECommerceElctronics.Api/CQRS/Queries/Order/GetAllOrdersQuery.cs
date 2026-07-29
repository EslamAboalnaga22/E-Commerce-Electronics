using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Queries.OrderFolder
{
    public record GetAllOrdersQuery : IRequest<IEnumerable<OrderDtoResponse>>;
}
