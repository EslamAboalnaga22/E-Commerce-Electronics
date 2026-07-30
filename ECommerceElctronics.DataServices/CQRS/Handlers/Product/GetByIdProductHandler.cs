using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Queries.ProductFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.ProductFolder
{
    public class GetByIdProductHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetByIdProductQuery, Result<GetProductDetailsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<GetProductDetailsResponse>> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetById(request.ProductId);

            if (product == null)
                return Result<GetProductDetailsResponse>.Failure(new("No Product Found", "Readed"));

            var result = _mapper.Map<GetProductDetailsResponse>(product);

            return Result<GetProductDetailsResponse>.Success(result);
        }
    }
}
