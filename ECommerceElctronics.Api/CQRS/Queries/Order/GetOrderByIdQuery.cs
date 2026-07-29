using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Queries.OrderFolder
{
    public record GetOrderByIdQuery(int Id) : IRequest<OrderDtoResponse>;
}
