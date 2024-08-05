using AutoMapper;
using ECommerceElctronics.Api.CQRS.Queries.CategoryFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.CategoryFolder
{
    public class GetByIdCategoryHandler : IRequestHandler<GetByIdCategoryQuery, Category>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByIdCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Category> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Categories.GetById(request.CategoryId);

            return category;
        }
    }
}
