using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Queries.ProductFolder
{
    public class GetByIdProductQuery : IRequest<GetProductDetailsResponse>
    {
        public int ProductId { get; set; }
        public GetByIdProductQuery(int productId)
        {
            ProductId = productId;
        }
    }
}
