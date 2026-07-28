using ECommerceElctronics.Entities.Dtos.Requests;
using ECommerceElctronics.Entities.Dtos.Responses;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.ProductFolder
{
    public class CreateProductCommand : IRequest<GetProductDetailsResponse>
    {
        public CreateProductRequest ProductRequest { get; set; }
        public CreateProductCommand(CreateProductRequest productRequest)
        {
            ProductRequest = productRequest;
        }
    }
}
