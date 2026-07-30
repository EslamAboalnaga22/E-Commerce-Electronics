using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.CategoryFolder
{
    public record DeleteCategoryCommand(int CategoryId) : IRequest<bool>;
}
