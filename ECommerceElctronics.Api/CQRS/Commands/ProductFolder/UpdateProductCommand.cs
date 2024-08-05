using ECommerceElctronics.Entities.Dtos.Requests;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.ProductFolder
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public int ProductIdRequest { get; set; }
        public UpdateProductRequest ProductRequest { get; set; }

        public UpdateProductCommand(int productIdRequest, UpdateProductRequest productRequest)
        {
            ProductIdRequest = productIdRequest;
            ProductRequest = productRequest;
        }
    }
}
