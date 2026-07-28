using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.BrandFolder
{
    public class DeleteBrandCommand : IRequest<bool>
    {
        public int BrandId { get; set; }
        public DeleteBrandCommand(int brandId)
        {
            BrandId = brandId;
        }
    }
}
