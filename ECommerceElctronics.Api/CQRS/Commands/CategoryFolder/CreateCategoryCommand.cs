using ECommerceElctronics.Entities.Dtos.Requests;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.CategoryFolder
{
    public class CreateCategoryCommand : IRequest<Category>
    {
        public CreateCategoryRequest CategoryRequest { get; set; }

        public CreateCategoryCommand(CreateCategoryRequest categoryRequest)
        {
            CategoryRequest = categoryRequest;
        }
    }
}
