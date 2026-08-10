using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Queries.BrandFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using MediatR;
using ECommerceElctronics.DataServices.Services.Caching;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.BrandFolder
{
    public class GetByIdBrandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheSerivces cacheSerivces) : IRequestHandler<GetByIdBrandQuery, Result<Brand>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ICacheSerivces _cacheSerivces = cacheSerivces;

        public async Task<Result<Brand>> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"brand-{request.BrandId}";

            var cachedBrand = _cacheSerivces.GetData<Brand>(cacheKey);

            if (cachedBrand.Value != null)
                return cachedBrand;

            var brand = await _unitOfWork.Brands.GetById(request.BrandId);

            if (brand == null)
                return Result<Brand>.Failure(new("No Brand Found", "Readed"));

            var expiryDate = DateTimeOffset.UtcNow.AddMinutes(10);

            _cacheSerivces.SetData(cacheKey, brand, expiryDate);

            return Result<Brand>.Success(brand);
        }
    }
}
