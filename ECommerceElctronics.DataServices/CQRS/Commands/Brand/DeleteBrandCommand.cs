using ECommerceElctronics.DataServices.ResultPattern;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Commands.BrandFolder
{
    public record DeleteBrandCommand(int BrandId) : IRequest<Result<bool>>;
}
