using MediatR;

namespace ECommerceElctronics.Api.CQRS.Commands.BrandFolder
{
    public record DeleteBrandCommand(int BrandId) : IRequest<bool>;
}
