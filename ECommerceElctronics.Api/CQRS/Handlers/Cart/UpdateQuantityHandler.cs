using ECommerceElctronics.Api.CQRS.Commands.Cart;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.Cart
{
    public class UpdateQuantityHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateQuantityCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<bool> Handle(UpdateQuantityCommand request, CancellationToken cancellationToken)
        {
            var caritem = await _unitOfWork.CartItems.GetById(request.QuantityRequest.CartItemId);

            if (caritem == null)
                throw new Exception("Cart Item Not Fount");

            caritem.Quantity = request.QuantityRequest.Quantity;
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
