using ECommerceElctronics.DataServices.CQRS.Commands.OrderFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.DataServices.ResultPattern;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.OrderFolder
{
    public class StatusProcessingOrderHandler(IUnitOfWork unitOfWork) : IRequestHandler<StatusProcessingOrderCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        async Task<Result<bool>> IRequestHandler<StatusProcessingOrderCommand, Result<bool>>.Handle(StatusProcessingOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetById(request.OrderId);

            if (order == null)
                return Result<bool>.Failure(new("Order Not Found", "Readed"));
            
            var result = order.StartProcessing();

            if(!result)
                return Result<bool>.Failure(new("Order Don't Created Or Paid", "Readed"));

            await _unitOfWork.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}
