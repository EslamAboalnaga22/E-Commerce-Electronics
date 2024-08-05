using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Queries.CategoryFolder
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<Category>>
    {
    }
}
