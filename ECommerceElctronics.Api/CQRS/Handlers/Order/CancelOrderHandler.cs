using ECommerceElctronics.Api.CQRS.Commands.OrderFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.OrderFolder
{
    public class CancelOrderHandler(IUnitOfWork unitOfWork) : IRequestHandler<CancelOrderCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        async Task<bool> IRequestHandler<CancelOrderCommand, bool>.Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetById(request.OrderId);

            if (order == null)
                throw new Exception("Order Not Found");

            if(order.Status == OrderStatus.Delivered || order.Status == OrderStatus.Shipped)
                throw new Exception("Cannot Cancel");

            order.Status = OrderStatus.Cancelled;

            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
