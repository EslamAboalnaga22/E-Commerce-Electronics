using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Commands.CategoryFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.CategoryFolder
{
    public class CreateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateCategoryCommand, Result<Category>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<Category>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<Category>(request.CategoryRequest);

            if (result == null)
                return Result<Category>.Failure(new("Something Wrong In Data", "Created"));

            await _unitOfWork.Categories.Add(result);
            await _unitOfWork.CompleteAsync();

            return Result<Category>.Success(result);
        }
     
    }
}
