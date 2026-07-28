using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.UserFolder
{
    public class CreateUserCommand : IRequest<User>
    {
        public User UserRequest { get; set; }

        public CreateUserCommand(User userRequest)
        {
            UserRequest = userRequest;
        }
    }
}
