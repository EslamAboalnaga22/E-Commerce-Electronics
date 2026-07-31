using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Queries.ProductFolder
{
    public record FilterProductByCategoryNameQuery(string Category) : IRequest<Result<IEnumerable<GetProductDetailsResponse>>>;
}
