using AutoMapper;
using ECommerceElctronics.Api.CQRS.Queries.OrderFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.OrderFolder
{
    public class GetOrderByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetOrderByIdQuery, OrderDtoResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<OrderDtoResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetById(request.Id);

            var result = _mapper.Map<OrderDtoResponse>(order);

            return result;
        }
    }
}
