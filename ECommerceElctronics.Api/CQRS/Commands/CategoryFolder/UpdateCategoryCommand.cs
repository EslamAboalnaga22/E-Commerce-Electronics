using ECommerceElctronics.Entities.Dtos.Requests;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.CategoryFolder
{
    public class UpdateCategoryCommand : IRequest<bool>
    {
        public Category CategoryRequest { get; set; }

        public UpdateCategoryCommand(Category categoryRequest)
        {
            CategoryRequest = categoryRequest;
        }
    }
}
