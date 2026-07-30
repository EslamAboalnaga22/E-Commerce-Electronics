using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Commands.OrderFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.OrderFolder
{
    public class CancelOrderHandler(IUnitOfWork unitOfWork) : IRequestHandler<CancelOrderCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        async Task<Result<bool>> IRequestHandler<CancelOrderCommand, Result<bool>>.Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetById(request.OrderId);

            if (order == null)
                return Result<bool>.Failure(new("Order Not Found", "Readed"));

            if(order.Status == OrderStatus.Delivered || order.Status == OrderStatus.Shipped)
                return Result<bool>.Failure(new("Cannot Cancel", "Readed"));

            order.Status = OrderStatus.Cancelled;

            await _unitOfWork.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}
