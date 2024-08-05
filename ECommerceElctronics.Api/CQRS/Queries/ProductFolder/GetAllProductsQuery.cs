using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Queries.ProductFolder
{
    public class GetAllProductsQuery : IRequest<IEnumerable<GetProductDetailsResponse>>
    {
    }
}
