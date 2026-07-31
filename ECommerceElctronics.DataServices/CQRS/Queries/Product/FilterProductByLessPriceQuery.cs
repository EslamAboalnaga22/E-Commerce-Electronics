using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Queries.ProductFolder
{
    public record FilterProductByLessPriceQuery(decimal Price) : IRequest<Result<IEnumerable<GetProductDetailsResponse>>>;
}
