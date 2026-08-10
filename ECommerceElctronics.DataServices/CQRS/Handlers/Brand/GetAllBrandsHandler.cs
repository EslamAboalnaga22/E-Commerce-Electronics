using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Queries.BrandFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using MediatR;
using ECommerceElctronics.DataServices.Services.Caching;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.BrandFolder
{
    public class GetAllBrandsHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheSerivces cacheSerivces) : IRequestHandler<GetAllBrandsQuery, Result<IEnumerable<Brand>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ICacheSerivces _cacheSerivces = cacheSerivces;

        public async Task<Result<IEnumerable<Brand>>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = "all-brands";

            var cachedBrands = _cacheSerivces.GetData<IEnumerable<Brand>>(cacheKey);

            if (cachedBrands.Value != null)
                return cachedBrands;

            var brands = await _unitOfWork.Brands.GetAll();

            if (brands == null)
                return Result <IEnumerable<Brand>>.Failure(new("No Brands Found", "Readed"));

            var expiryDate = DateTimeOffset.UtcNow.AddMinutes(10);

            _cacheSerivces.SetData(cacheKey, brands, expiryDate);

            return Result<IEnumerable<Brand>>.Success(brands);
        }
    }
}
