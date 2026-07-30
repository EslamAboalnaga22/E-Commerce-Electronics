using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Queries.OrderFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.OrderFolder
{
    public class GetAllOrdersHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllOrdersQuery, Result<IEnumerable<OrderDtoResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<OrderDtoResponse>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Orders.GetAll();

            if (orders == null)
                return Result<IEnumerable<OrderDtoResponse>>.Failure(new("No Orders Found", "Readed"));

            var results = _mapper.Map<IEnumerable<OrderDtoResponse>>(orders);

            return Result<IEnumerable<OrderDtoResponse>>.Success(results);
        }
    }
}
