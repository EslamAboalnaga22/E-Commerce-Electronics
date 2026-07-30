using ECommerceElctronics.Api.CQRS.Commands.Cart;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.Cart
{
    public class ClearCartHandler(IUnitOfWork unitOfWork) : IRequestHandler<ClearCartCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<bool> Handle(ClearCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.Carts.GetCartByUserId(request.UserId);

            if (cart == null)
                throw new Exception("Cart Not Found");

            await _unitOfWork.CartItems.DeletCartItems(cart.Id);

            return true;
        }
    }
}
