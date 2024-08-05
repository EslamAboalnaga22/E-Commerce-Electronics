using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.BrandFolder
{
    public class UpdateBrandCommand : IRequest<bool>
    {
        public Brand BrandRequest { get; set; }
        public UpdateBrandCommand(Brand brandRequest)
        {
            BrandRequest = brandRequest;
        }
    }
}
