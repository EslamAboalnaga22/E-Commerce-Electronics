using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Queries.BrandFolder
{
    public class GetByIdBrandQuery : IRequest<Brand>
    {
        public int BrandId { get; set; }
        public GetByIdBrandQuery(int brandId)
        {
            BrandId = brandId;
        }
    }
}
