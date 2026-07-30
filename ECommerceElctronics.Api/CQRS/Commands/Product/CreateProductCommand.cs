using ECommerceElctronics.Entities.Dtos.Requests;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.ProductFolder
{
    public record CreateProductCommand(CreateProductRequest ProductRequest) : IRequest<GetProductDetailsResponse>;
}
