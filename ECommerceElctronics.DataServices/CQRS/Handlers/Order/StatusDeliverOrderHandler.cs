using ECommerceElctronics.DataServices.CQRS.Commands.OrderFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.DataServices.ResultPattern;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.OrderFolder
{
    public class StatusDeliverOrderHandler(IUnitOfWork unitOfWork) : IRequestHandler<StatusDeliverOrderCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        async Task<Result<bool>> IRequestHandler<StatusDeliverOrderCommand, Result<bool>>.Handle(StatusDeliverOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetById(request.OrderId);

            if (order == null)
                return Result<bool>.Failure(new("Order Not Found", "Readed"));
            
            var result = order.Deliver();

            if(!result)
                return Result<bool>.Failure(new("Order Don't Shipped", "Readed"));

            await _unitOfWork.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}
