using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Commands.UserFolder
{
    public record CreateUserCommand(User UserRequest) : IRequest<Result<User>>;
}
