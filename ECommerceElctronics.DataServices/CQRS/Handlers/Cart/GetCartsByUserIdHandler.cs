using AutoMapper;
using ECommerceElctronics.DataServices.CQRS.Queries.CartFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.Services.Caching;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.CartFolder
{
    public class GetCartsByUserIdHandler(IUnitOfWork unitOfWork, IMapper mapper, IRedisServices redisServices) : IRequestHandler<GetCartsByUserIdQuery, Result<CartDtoResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IRedisServices _redisServices = redisServices;

        public async Task<Result<CartDtoResponse>> Handle(GetCartsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"cart_{request.UserId}";

            var cachedCart = _redisServices.GetData<CartDtoResponse>(cacheKey);

            if (cachedCart.Value != null)
                return cachedCart;

            var cart = await _unitOfWork.Carts.GetCartByUserId(request.UserId);

            if (cart == null)
                return Result<CartDtoResponse>.Failure(new("Cart Not Found", "Readed"));

            var result = _mapper.Map<CartDtoResponse>(cart);

            _redisServices.SetData(cacheKey, result);

            return Result<CartDtoResponse>.Success(result);
        }
    }
}
