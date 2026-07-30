using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Queries.OrderFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.OrderFolder
{
    public class GetOrderByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetOrderByIdQuery, Result<OrderDtoResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<OrderDtoResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetById(request.Id);

            if (order == null)
                return Result<OrderDtoResponse>.Failure(new("No Order Found", "Readed"));

            var result = _mapper.Map<OrderDtoResponse>(order);

            return Result<OrderDtoResponse>.Success(result);
        }
    }
}
