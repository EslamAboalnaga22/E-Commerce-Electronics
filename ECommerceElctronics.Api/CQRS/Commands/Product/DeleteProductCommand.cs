using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.ProductFolder
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public int ProductId { get; set; }

        public DeleteProductCommand(int productId)
        {
            ProductId = productId;
        }
    }
}
