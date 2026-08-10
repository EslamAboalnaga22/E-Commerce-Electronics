using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Commands.Cart;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using MediatR;
using ECommerceElctronics.DataServices.Services.Caching;

namespace ECommerceElctronics.ApDataServicesi.CQRS.Handlers.Cart
{
    public class UpdateQuantityHandler(IUnitOfWork unitOfWork, IRedisServices redisServices) : IRequestHandler<UpdateQuantityCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IRedisServices _redisServices = redisServices;
        public async Task<Result<bool>> Handle(UpdateQuantityCommand request, CancellationToken cancellationToken)
        {
            var caritem = await _unitOfWork.CartItems.GetById(request.QuantityRequest.CartItemId);

            if (caritem == null)
               return Result<bool>.Failure(new("CartItem Not Found", "Readed")); ;

            caritem.Quantity = request.QuantityRequest.Quantity;
            await _unitOfWork.CompleteAsync();


            var cacheKeyUser = $"cart_{caritem.Cart.UserId}";
            _redisServices.RemoveData(cacheKeyUser);

            var cacheKeyCart = $"cart_{caritem.Cart.Id}";
            _redisServices.RemoveData(cacheKeyCart);

            var cacheKeyCarts = $"all-Carts";
            _redisServices.RemoveData(cacheKeyCarts);


            return Result<bool>.Success(true);
        }
    }
}
