using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Queries.BrandFolder
{
    public record GetAllBrandsQuery : IRequest<Result<IEnumerable<Brand>>>;
}
