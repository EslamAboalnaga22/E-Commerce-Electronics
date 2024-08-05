using AutoMapper;
using ECommerceElctronics.Api.CQRS.Queries.CartFolder;
using ECommerceElctronics.Api.CQRS.Queries.OrderFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.CartFolder
{
    public class GetAllCartsHandler : IRequestHandler<GetAllCartsQuery, IEnumerable<GetCartDetailsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCartsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetCartDetailsResponse>> Handle(GetAllCartsQuery request, CancellationToken cancellationToken)
        {
            var carts = await _unitOfWork.Carts.GetAll();

            var results = _mapper.Map<IEnumerable<GetCartDetailsResponse>>(carts);

            foreach (var result in results)
            {
                var orders = await _unitOfWork.Orders.GetOrderByUserId(result.UserId);
                var map = _mapper.Map<IEnumerable<GetOrderDetailssResponse>>(orders);
                foreach (var item in map)
                {
                    result.OrdersDetails.Add(item);
                }
            }

            return _mapper.Map<IEnumerable<GetCartDetailsResponse>>(results);
        }
    }
}
