using AutoMapper;
using ECommerceElctronics.Api.CQRS.Queries.ProductFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.ProductFolder
{
    public class GetByIdProductHandler : IRequestHandler<GetByIdProductQuery, GetProductDetailsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByIdProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetProductDetailsResponse> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetById(request.ProductId);
            
            if (product == null)
                return null;

            var result = _mapper.Map<GetProductDetailsResponse>(product);

            return result;
        }
    }
}
