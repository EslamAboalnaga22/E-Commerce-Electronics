using ECommerceElctronics.Api.CQRS.Commands.Cart;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.Cart
{
    public class RemoveItemFromCartHandler(IUnitOfWork unitOfWork) : IRequestHandler<RemoveItemFromCartCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<bool> Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
        {
            var caritem = await _unitOfWork.CartItems.GetById(request.CartItemId);

            if (caritem == null)
                throw new Exception("Cart Item Not Fount");

            await _unitOfWork.CartItems.Delete(caritem);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
