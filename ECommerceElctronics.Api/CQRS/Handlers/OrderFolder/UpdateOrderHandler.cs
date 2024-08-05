using AutoMapper;
using ECommerceElctronics.Api.CQRS.Commands.OrderFolder;
using ECommerceElctronics.Api.CQRS.Commands.ProductFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.OrderFolder
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateOrderHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetById(request.OrderIdRequest);

            var result = _mapper.Map<Order>(request.OrderRequest);

            result.Id = request.OrderIdRequest;

            await _unitOfWork.Orders.Update(result);

            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
