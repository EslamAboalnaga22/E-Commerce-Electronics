using ECommerceElctronics.Entities.Dtos.Responses;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Queries.CartFolder
{
    public class GetAllCartsQuery : IRequest<IEnumerable<GetCartDetailsResponse>>
    {

    }
}
