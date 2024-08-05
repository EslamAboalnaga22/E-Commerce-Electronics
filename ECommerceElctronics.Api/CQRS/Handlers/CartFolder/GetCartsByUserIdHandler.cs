using AutoMapper;
using ECommerceElctronics.Api.CQRS.Queries.CartFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.CartFolder
{
    public class GetCartsByUserIdHandler : IRequestHandler<GetCartsByUserIdQuery, IEnumerable<GetCartDetailsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCartsByUserIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetCartDetailsResponse>> Handle(GetCartsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.Carts.GetCartByUserId(request.UserId);

            var results = _mapper.Map<IEnumerable<GetCartDetailsResponse>>(cart);

            foreach (var result in results)
            {
                var orders = await _unitOfWork.Orders.GetOrderByCartId(result.Id);
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
