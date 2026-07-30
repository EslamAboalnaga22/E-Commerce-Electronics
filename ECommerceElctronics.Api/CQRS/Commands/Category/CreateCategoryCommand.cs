using ECommerceElctronics.Entities.Dtos.Requests;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.CategoryFolder
{
    public record CreateCategoryCommand(CreateCategoryRequest CategoryRequest) : IRequest<Category>;
}
