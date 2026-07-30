using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Commands.OrderFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.OrderFolder
{
    public class DeleteOrderHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<DeleteOrderCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<bool>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetById(request.OrderId);

            if (order == null)
                return Result<bool>.Failure(new("Order Not Found", "Readed"));

            await _unitOfWork.Orders.Delete(order);
            await _unitOfWork.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}
