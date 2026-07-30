using AutoMapper;
using ECommerceElctronics.Api.CQRS.Queries.CartFolder;
using ECommerceElctronics.Api.CQRS.Queries.OrderFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.CartFolder
{
    public class GetAllCartsHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllCartsQuery, IEnumerable<CartDtoResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<CartDtoResponse>> Handle(GetAllCartsQuery request, CancellationToken cancellationToken)
        {
            var carts = await _unitOfWork.Carts.GetAll();

            var results = _mapper.Map<IEnumerable<CartDtoResponse>>(carts);

            return results;
        }
    }
}
