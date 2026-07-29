using ECommerceElctronics.Api.CQRS.Commands.Cart;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.Cart
{
    public class CheckoutHandler(IUnitOfWork unitOfWork) : IRequestHandler<CheckoutCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<int> Handle(CheckoutCommand request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.Carts.GetCartByUserId(request.UserId);

            if (cart == null || !cart.Items.Any())
                throw new Exception("Cart Empty");

            var order = new Order
            {
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Pending,
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

            return order.Id;
        }
    }
}
