using AutoMapper;
using ECommerceElctronics.Api.CQRS.Queries.OrderFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.OrderFolder
{
    public class GetUserOrdersHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetUserOrdersQuery, IEnumerable<OrderDtoResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<OrderDtoResponse>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Orders.GetOrderByUserId(request.UserId);

            var result = _mapper.Map<IEnumerable<OrderDtoResponse>>(orders);

            return result;
        }
    }
}
