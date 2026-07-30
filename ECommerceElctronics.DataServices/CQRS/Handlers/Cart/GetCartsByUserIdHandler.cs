using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Queries.CartFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.CartFolder
{
    public class GetCartsByUserIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetCartsByUserIdQuery, Result<CartDtoResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<CartDtoResponse>> Handle(GetCartsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.Carts.GetCartByUserId(request.UserId);

            if (cart == null)
                return Result<CartDtoResponse>.Failure(new("Cart Not Found", "Readed"));

            var result = _mapper.Map<CartDtoResponse>(cart);

            return Result<CartDtoResponse>.Success(result);
        }
    }
}
