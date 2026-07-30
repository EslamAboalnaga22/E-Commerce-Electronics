using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Commands.CategoryFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.CategoryFolder
{
    public class UpdateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateCategoryCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Categories.Update(request.CategoryRequest);
            await _unitOfWork.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}
