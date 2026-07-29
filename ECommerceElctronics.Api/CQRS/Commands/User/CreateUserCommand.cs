using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.UserFolder
{
    public record CreateUserCommand(User UserRequest) : IRequest<User>;
}
