using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Queries.CategoryFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.CategoryFolder
{
    public class GetAllCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllCategoriesQuery, Result<IEnumerable<Category>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<Category>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.Categories.GetAll();

            if (categories == null)
                return Result<IEnumerable<Category>>.Failure(new("No Categories Found", "Readed"));

            return Result<IEnumerable<Category>>.Success(categories);
        }
    }
}
