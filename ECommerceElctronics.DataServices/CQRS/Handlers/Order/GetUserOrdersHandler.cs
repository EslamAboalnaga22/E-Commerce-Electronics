using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Queries.OrderFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.OrderFolder
{
    public class GetUserOrdersHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetUserOrdersQuery, Result<IEnumerable<OrderDtoResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<OrderDtoResponse>>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetOrderByUserId(request.UserId);

            if (order == null)
                return Result<IEnumerable<OrderDtoResponse>>.Failure(new("Order Not Found", "Readed"));

            var result = _mapper.Map<IEnumerable<OrderDtoResponse>>(order);

            return Result<IEnumerable<OrderDtoResponse>>.Success(result);
        }
    }
}
