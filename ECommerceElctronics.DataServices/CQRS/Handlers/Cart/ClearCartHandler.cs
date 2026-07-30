using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Commands.Cart;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.Cart
{
    public class ClearCartHandler(IUnitOfWork unitOfWork) : IRequestHandler<ClearCartCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Result<bool>> Handle(ClearCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.Carts.GetCartByUserId(request.UserId);

            if (cart == null)
                return Result<bool>.Failure(new("Cart Not Found", "Readed"));

            await _unitOfWork.CartItems.DeletCartItems(cart.Id);

            return Result<bool>.Success(true);
        }
    }
}
