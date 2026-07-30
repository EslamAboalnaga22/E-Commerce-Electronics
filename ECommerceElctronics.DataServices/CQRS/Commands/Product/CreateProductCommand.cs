using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Dtos.Requests;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Commands.ProductFolder
{
    public record CreateProductCommand(CreateProductRequest ProductRequest) : IRequest<Result<GetProductDetailsResponse>>;
}
