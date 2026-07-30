using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Commands.CategoryFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.CategoryFolder
{
    public class DeleteCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<DeleteCategoryCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Categories.GetById(request.CategoryId);

            if (category == null)
               return Result<bool>.Failure(new("Brand Not Found", "Deleted")); ;

            await _unitOfWork.Categories.Delete(category);
            await _unitOfWork.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}
