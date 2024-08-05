using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.CategoryFolder
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public int CategoryId { get; set; }

        public DeleteCategoryCommand(int categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
