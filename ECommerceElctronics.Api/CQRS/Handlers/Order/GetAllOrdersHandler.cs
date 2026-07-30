using AutoMapper;
using ECommerceElctronics.Api.CQRS.Queries.OrderFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.OrderFolder
{
    public class GetAllOrdersHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDtoResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<OrderDtoResponse>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Orders.GetAll();

            return _mapper.Map<IEnumerable<OrderDtoResponse>>(orders);
        }
    }
}
