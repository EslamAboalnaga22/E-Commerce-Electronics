using AutoMapper;
using ECommerceElctronics.Api.CQRS.Queries.CartFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.CartFolder
{
    public class GetCartsByUserIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetCartsByUserIdQuery, CartDtoResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<CartDtoResponse> Handle(GetCartsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.Carts.GetCartByUserId(request.UserId);

            var result = _mapper.Map<CartDtoResponse>(cart);

            return result;
        }
    }
}
