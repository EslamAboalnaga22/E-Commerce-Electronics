using AutoMapper;
using ECommerceElctronics.Api.CQRS.Queries.CartFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.CartFolder
{
    public class GetCartsByCartdHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetCartsByCartIdQuery, CartDtoResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<CartDtoResponse> Handle(GetCartsByCartIdQuery request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.Carts.GetById(request.CartId);

            var result = _mapper.Map<CartDtoResponse>(cart);

            return result;
        }
    }
}
