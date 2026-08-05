using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Commands.OrderFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.OrderFolder
{
    public class ChangeOrderStatusHandler(IUnitOfWork unitOfWork) : IRequestHandler<ChangeOrderStatusCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        async Task<Result<bool>> IRequestHandler<ChangeOrderStatusCommand, Result<bool>>.Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetById(request.OrderId);

            if (order == null)
                return Result<bool>.Failure(new("Order Not Found", "Readed"));

            //order.Status = request.Status;

            await _unitOfWork.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}