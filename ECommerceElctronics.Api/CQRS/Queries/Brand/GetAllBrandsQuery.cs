using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Queries.BrandFolder
{
    public class GetAllBrandsQuery : IRequest<IEnumerable<Brand>>
    {
    }
}
