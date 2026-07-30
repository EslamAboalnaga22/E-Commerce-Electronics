using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Commands.OrderFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.OrderFolder
{
    public class UpdateOrderHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateOrderCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<bool>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetById(request.OrderIdRequest);

            if (order == null)
                return Result<bool>.Failure(new("Order Not Found", "Readed"));

            var result = _mapper.Map<Order>(request.OrderRequest);

            result.Id = request.OrderIdRequest;

            await _unitOfWork.Orders.Update(result);
            await _unitOfWork.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}
