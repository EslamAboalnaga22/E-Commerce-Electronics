using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Queries.BrandFolder
{
    public record GetByIdBrandQuery(int BrandId) : IRequest<Result<Brand>>;
}
