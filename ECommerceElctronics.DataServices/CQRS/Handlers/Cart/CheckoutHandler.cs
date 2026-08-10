using ECommerceElctronics.DataServices.CQRS.Commands.Cart;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.Services.Caching;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.Cart
{
    public class CheckoutHandler(IUnitOfWork unitOfWork, IRedisServices redisServices) : IRequestHandler<CheckoutCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IRedisServices _redisServices = redisServices;
        public async Task<Result<int>> Handle(CheckoutCommand request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.Carts.GetCartByUserId(request.UserId);

            if (cart == null || !cart.Items.Any())
                return Result<int>.Failure(new("Cart is Empty Or Not Found", "Readed"));

            var order = new Order
            {
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow,
            };

            decimal total = 0;

            foreach (var item in cart.Items)
            {
                order.Items.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                });

                total += item.Quantity * item.UnitPrice;
            }

            order.TotalPrice = total;

            await _unitOfWork.Orders.Add(order);

            await _unitOfWork.CartItems.DeletCartItems(cart.Id);

            await _unitOfWork.CompleteAsync();

            var cacheKeyUser = $"cart_{request.UserId}";
            _redisServices.RemoveData(cacheKeyUser);

            var cacheKeyCart = $"cart_{cart.Id}";
            _redisServices.RemoveData(cacheKeyCart);

            var cacheKeyCarts = $"all-Carts";
            _redisServices.RemoveData(cacheKeyCarts);

            return Result<int>.Success(order.Id);
        }
    }
}
