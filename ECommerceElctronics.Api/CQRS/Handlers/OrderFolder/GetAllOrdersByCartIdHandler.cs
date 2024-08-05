using AutoMapper;
using ECommerceElctronics.Api.CQRS.Queries.OrderFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.OrderFolder
{
    public class GetAllOrdersByCartIdHandler : IRequestHandler<GetAllOrdersByCartIdQuery, IEnumerable<GetOrderDetailssResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllOrdersByCartIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetOrderDetailssResponse>> Handle(GetAllOrdersByCartIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Orders.GetOrderByCartId(request.CartId);

            var result = _mapper.Map<IEnumerable<GetOrderDetailssResponse>>(orders);

            return result;
        }
    }
}
