using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Queries.CartFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.CartFolder
{
    public class GetCartsByCartdHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetCartsByCartIdQuery, Result<CartDtoResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<CartDtoResponse>> Handle(GetCartsByCartIdQuery request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.Carts.GetById(request.CartId);

            if (cart == null)
                return Result<CartDtoResponse>.Failure(new("Cart Not Found", "Readed"));

            var result = _mapper.Map<CartDtoResponse>(cart);

            return Result<CartDtoResponse>.Success(result);
        }
    }
}
