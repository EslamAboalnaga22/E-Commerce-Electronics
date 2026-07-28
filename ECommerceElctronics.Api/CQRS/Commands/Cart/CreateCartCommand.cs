using ECommerceElctronics.Entities.Dtos.Requests;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.CartFolder
{
    public class CreateCartCommand : IRequest<Cart>
    {
        public CreateCartRequest CartRequest { get; set; }

        public CreateCartCommand(CreateCartRequest cartRequest )
        {
            CartRequest = cartRequest;
        }
    }
}
