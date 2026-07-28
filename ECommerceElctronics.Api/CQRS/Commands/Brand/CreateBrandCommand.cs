using ECommerceElctronics.Entities.Dtos.Requests;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.BrandFolder
{
    public class CreateBrandCommand : IRequest<Brand>
    {
        public CreateBrandRequest BrandRequest { get; }
        public CreateBrandCommand(CreateBrandRequest brandRequest)
        {
            BrandRequest = brandRequest;
        }
    }
}
