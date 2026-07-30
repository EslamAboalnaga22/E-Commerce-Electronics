using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Queries.ProductFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.ProductFolder
{
    public class GetAllProductsHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllProductsQuery, Result<IEnumerable<GetProductDetailsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<GetProductDetailsResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.Products.GetAll();

            var result = _mapper.Map<IEnumerable<GetProductDetailsResponse>>(products);

            if (products == null)
                return Result<IEnumerable<GetProductDetailsResponse>>.Failure(new("No Products Found", "Readed"));

            return Result<IEnumerable<GetProductDetailsResponse>>.Success(result);
        }
    }
}
