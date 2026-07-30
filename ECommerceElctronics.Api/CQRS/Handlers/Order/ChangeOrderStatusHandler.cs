using ECommerceElctronics.Api.CQRS.Commands.OrderFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.OrderFolder
{
    public class ChangeOrderStatusHandler(IUnitOfWork unitOfWork) : IRequestHandler<ChangeOrderStatusCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        async Task<bool> IRequestHandler<ChangeOrderStatusCommand, bool>.Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetById(request.OrderId);

            if (order == null)
                throw new Exception("Order Not Found");

            order.Status = request.Status;

            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}