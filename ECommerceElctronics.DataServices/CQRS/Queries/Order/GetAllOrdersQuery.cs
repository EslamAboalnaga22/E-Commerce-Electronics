using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Queries.OrderFolder
{
    public record GetAllOrdersQuery : IRequest<Result<IEnumerable<OrderDtoResponse>>>;
}
