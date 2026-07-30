using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Dtos.Requests;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Commands.CategoryFolder
{
    public record CreateCategoryCommand(CreateCategoryRequest CategoryRequest) : IRequest<Result<Category>>;
}
