using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Queries.ProductFolder
{
    public record FilterProductByBrandNameQuery(string Brand) : IRequest<Result<IEnumerable<GetProductDetailsResponse>>>;
}
