using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Queries.ProductFolder
{
    public record GetAllProductsQuery : IRequest<Result<IEnumerable<GetProductDetailsResponse>>>;
}
