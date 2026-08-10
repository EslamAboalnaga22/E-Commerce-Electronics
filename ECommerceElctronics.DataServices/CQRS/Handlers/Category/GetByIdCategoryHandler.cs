using AutoMapper;
using ECommerceElctronics.DataServices.CQRS.Queries.CategoryFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.Services.Caching;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.CategoryFolder
{
    public class GetByIdCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheSerivces cacheSerivces) : IRequestHandler<GetByIdCategoryQuery, Result<Category>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ICacheSerivces _cacheSerivces = cacheSerivces;

        public async Task<Result<Category>> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"category-{request.CategoryId}";

            var cachedCategory = _cacheSerivces.GetData<Category>(cacheKey);

            if (cachedCategory.Value != null)
                return cachedCategory;

            var category = await _unitOfWork.Categories.GetById(request.CategoryId);

            if (category == null)
                return Result<Category>.Failure(new("No Category Found", "Readed"));

            var expiryDate = DateTimeOffset.UtcNow.AddMinutes(10);

            _cacheSerivces.SetData(cacheKey, category, expiryDate);

            return Result<Category>.Success(category);
        }
    }
}
