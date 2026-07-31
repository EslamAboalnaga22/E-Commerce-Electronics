using AutoMapper;
using ECommerceElctronics.DataServices.CQRS.Queries.ProductFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.ProductFolder
{
    public class FilterProductByCategoryNameHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<FilterProductByCategoryNameQuery, Result<IEnumerable<GetProductDetailsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<GetProductDetailsResponse>>> Handle(FilterProductByCategoryNameQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.Products.FilterProductByCategoryName(request.Category);

            if (products == null)
                return Result<IEnumerable<GetProductDetailsResponse>>.Failure(new("Products Not Found", "Readed"));

            var result = _mapper.Map<IEnumerable<GetProductDetailsResponse>>(products);

            return Result<IEnumerable<GetProductDetailsResponse>>.Success(result);
        }
    }
}
