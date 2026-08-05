using ECommerceElctronics.DataServices.CQRS.Commands.OrderFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.OrderFolder
{
    public class StatusPayOrderHandler(IUnitOfWork unitOfWork) : IRequestHandler<StatusPayOrderCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        async Task<Result<bool>> IRequestHandler<StatusPayOrderCommand, Result<bool>>.Handle(StatusPayOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetById(request.OrderId);

            if (order == null)
                return Result<bool>.Failure(new("Order Not Found", "Readed"));
            
            var result = order.Pay();

            if(!result)
                return Result<bool>.Failure(new("Order Don't Created", "Readed"));

            await _unitOfWork.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}
