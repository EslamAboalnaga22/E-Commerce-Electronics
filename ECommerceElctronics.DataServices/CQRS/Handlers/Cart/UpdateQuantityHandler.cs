using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Commands.Cart;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using MediatR;

namespace ECommerceElctronics.ApDataServicesi.CQRS.Handlers.Cart
{
    public class UpdateQuantityHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateQuantityCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Result<bool>> Handle(UpdateQuantityCommand request, CancellationToken cancellationToken)
        {
            var caritem = await _unitOfWork.CartItems.GetById(request.QuantityRequest.CartItemId);

            if (caritem == null)
               return Result<bool>.Failure(new("CartItem Not Found", "Readed")); ;

            caritem.Quantity = request.QuantityRequest.Quantity;
            await _unitOfWork.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}
