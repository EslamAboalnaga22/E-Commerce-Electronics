using ECommerceElctronics.DataServices.ResultPattern;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Commands.CategoryFolder
{
    public record DeleteCategoryCommand(int CategoryId) : IRequest<Result<bool>>;
}
