using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Queries.CategoryFolder
{
    public record GetAllCategoriesQuery : IRequest<Result<IEnumerable<Category>>>;
}
