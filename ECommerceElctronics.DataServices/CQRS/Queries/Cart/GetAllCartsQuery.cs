using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Queries.CartFolder
{
    public record GetAllCartsQuery : IRequest<Result<IEnumerable<CartDtoResponse>>>;
}
