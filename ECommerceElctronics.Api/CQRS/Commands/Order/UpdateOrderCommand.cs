using ECommerceElctronics.Entities.Dtos.Requests;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.OrderFolder
{
    public class UpdateOrderCommand : IRequest<bool>
    {
        public int OrderIdRequest { get; set; }
        public UpdateOrderRequest OrderRequest { get; set; }

        public UpdateOrderCommand(int orderIdRequest, UpdateOrderRequest orderRequest)
        {
            OrderIdRequest = orderIdRequest;
            OrderRequest = orderRequest;
        }
    }
}
