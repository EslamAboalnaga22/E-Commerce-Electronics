using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Queries.ProductFolder
{
    public record GetByIdProductQuery(int ProductId) : IRequest<Result<GetProductDetailsResponse>>;
}
