using ECommerceElctronics.DataServices.CQRS.Commands.Cart;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.Services.Caching;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.Cart
{
    public class RemoveItemFromCartHandler(IUnitOfWork unitOfWork, IRedisServices redisServices) : IRequestHandler<RemoveItemFromCartCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IRedisServices _redisServices = redisServices;
        public async Task<Result<bool>> Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
        {
            var caritem = await _unitOfWork.CartItems.GetById(request.CartItemId);

            if (caritem == null)
                return Result<bool>.Failure(new("Cart Not Found", "Readed"));

            var cacheKeyUser = $"cart_{caritem.Cart.UserId}";
            _redisServices.RemoveData(cacheKeyUser);

            var cacheKeyCart = $"cart_{caritem.Cart.Id}";
            _redisServices.RemoveData(cacheKeyCart);

            var cacheKeyCarts = $"all-Carts";
            _redisServices.RemoveData(cacheKeyCarts);

            await _unitOfWork.CartItems.Delete(caritem);
            await _unitOfWork.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}
