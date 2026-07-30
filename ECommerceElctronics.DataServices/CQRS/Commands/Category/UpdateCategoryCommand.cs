using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Commands.CategoryFolder
{
    public record UpdateCategoryCommand(Category CategoryRequest) : IRequest<Result<bool>>;
}
