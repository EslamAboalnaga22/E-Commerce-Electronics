using AutoMapper;
using ECommerceElctronics.Api.CQRS.Queries.ProductFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.ProductFolder
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<GetProductDetailsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllProductsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetProductDetailsResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.Products.GetAll();

            var result = _mapper.Map<IEnumerable<GetProductDetailsResponse>>(products);

            return result;
        }
    }
}
