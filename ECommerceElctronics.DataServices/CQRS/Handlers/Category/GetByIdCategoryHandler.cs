using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Queries.CategoryFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.CategoryFolder
{
    public class GetByIdCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetByIdCategoryQuery, Result<Category>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<Category>> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Categories.GetById(request.CategoryId);

            if (category == null)
                return Result<Category>.Failure(new("No Category Found", "Readed"));

            return Result<Category>.Success(category);
        }
    }
}
