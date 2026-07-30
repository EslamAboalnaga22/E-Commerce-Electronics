using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Commands.Cart;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.Cart
{
    public class RemoveItemFromCartHandler(IUnitOfWork unitOfWork) : IRequestHandler<RemoveItemFromCartCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Result<bool>> Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
        {
            var caritem = await _unitOfWork.CartItems.GetById(request.CartItemId);

            if (caritem == null)
                return Result<bool>.Failure(new("Cart Not Found", "Readed"));

            await _unitOfWork.CartItems.Delete(caritem);
            await _unitOfWork.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}
