using AutoMapper;
using ECommerceElctronics.Api.CQRS.Queries.OrderFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.OrderFolder
{
    public class GetAllOrdersByUserIdHandler : IRequestHandler<GetAllOrdersByUserIdQuery, IEnumerable<GetOrderDetailssResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllOrdersByUserIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetOrderDetailssResponse>> Handle(GetAllOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Orders.GetOrderByUserId(request.UserId);

            var result = _mapper.Map<IEnumerable<GetOrderDetailssResponse>>(orders);

            return result;
        }
    }
}
