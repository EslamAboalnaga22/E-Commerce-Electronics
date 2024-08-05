using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Queries.CategoryFolder
{
    public class GetByIdCategoryQuery : IRequest<Category>
    {
        public int CategoryId { get; set; }

        public GetByIdCategoryQuery(int categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
