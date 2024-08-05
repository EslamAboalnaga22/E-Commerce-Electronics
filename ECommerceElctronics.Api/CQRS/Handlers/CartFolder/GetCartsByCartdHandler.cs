using AutoMapper;
using ECommerceElctronics.Api.CQRS.Queries.CartFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.CartFolder
{
    public class GetCartsByCartdHandler : IRequestHandler<GetCartsByCartIdQuery, GetCartDetailsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCartsByCartdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetCartDetailsResponse> Handle(GetCartsByCartIdQuery request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.Carts.GetById(request.CartId);

            var result = _mapper.Map<GetCartDetailsResponse>(cart);

            var orders = await _unitOfWork.Orders.GetOrderByCartId(result.Id);

            var map = _mapper.Map<IEnumerable<GetOrderDetailssResponse>>(orders);

            foreach (var item in map)
            {
                result.OrdersDetails.Add(item);
            }

            return _mapper.Map<GetCartDetailsResponse>(result);
        }
    }
}
