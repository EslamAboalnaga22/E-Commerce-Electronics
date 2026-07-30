using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Queries.CartFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.CartFolder
{
    public class GetAllCartsHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllCartsQuery, Result<IEnumerable<CartDtoResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<CartDtoResponse>>> Handle(GetAllCartsQuery request, CancellationToken cancellationToken)
        {
            var carts = await _unitOfWork.Carts.GetAll();

            if (carts == null)
                return Result <IEnumerable<CartDtoResponse>>.Failure(new("Carts Not Found", "Readed"));

            var results = _mapper.Map<IEnumerable<CartDtoResponse>>(carts);

            return Result<IEnumerable<CartDtoResponse>>.Success(results);
        }
    }
}
