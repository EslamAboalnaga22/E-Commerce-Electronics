using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Commands.OrderFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.OrderFolder
{
    public class StatusCancelOrderHandler(IUnitOfWork unitOfWork) : IRequestHandler<StatusCancelOrderCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        async Task<Result<bool>> IRequestHandler<StatusCancelOrderCommand, Result<bool>>.Handle(StatusCancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetById(request.OrderId);

            if (order == null)
                return Result<bool>.Failure(new("Order Not Found", "Readed"));

            if(order.Status == OrderStatus.Delivered || order.Status == OrderStatus.Shipped)
                return Result<bool>.Failure(new("Cannot Cancel", "Readed"));

            var result = order.Cancel();

            if (!result)
                return Result<bool>.Failure(new("Order Maybe Shipped Or Deliverd", "Readed"));

            await _unitOfWork.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}
