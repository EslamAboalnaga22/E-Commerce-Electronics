using ECommerceElctronics.DataServices.CQRS.Commands.OrderFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.DataServices.ResultPattern;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.OrderFolder
{
    public class StatusShipOrderHandler(IUnitOfWork unitOfWork) : IRequestHandler<StatusShipOrderCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        async Task<Result<bool>> IRequestHandler<StatusShipOrderCommand, Result<bool>>.Handle(StatusShipOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetById(request.OrderId);

            if (order == null)
                return Result<bool>.Failure(new("Order Not Found", "Readed"));
            
            var result = order.Ship();

            if(!result)
                return Result<bool>.Failure(new("Order Don't Processed", "Readed"));

            await _unitOfWork.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}
