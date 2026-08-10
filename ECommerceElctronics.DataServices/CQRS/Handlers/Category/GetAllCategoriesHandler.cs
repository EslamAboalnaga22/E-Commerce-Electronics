using AutoMapper;
using ECommerceElctronics.DataServices.CQRS.Queries.CategoryFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.Services.Caching;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.CategoryFolder
{
    public class GetAllCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheSerivces cacheSerivces) : IRequestHandler<GetAllCategoriesQuery, Result<IEnumerable<Category>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ICacheSerivces _cacheSerivces = cacheSerivces;

        public async Task<Result<IEnumerable<Category>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = "all-categories";

            var cachedCategories = _cacheSerivces.GetData<IEnumerable<Category>>(cacheKey);

            if (cachedCategories.Value != null)
                return cachedCategories;

            var categories = await _unitOfWork.Categories.GetAll();

            if (categories == null)
                return Result<IEnumerable<Category>>.Failure(new("No Categories Found", "Readed"));

            var expiryDate = DateTimeOffset.UtcNow.AddMinutes(10);

            _cacheSerivces.SetData(cacheKey, categories, expiryDate);

            return Result<IEnumerable<Category>>.Success(categories);
        }
    }
}
