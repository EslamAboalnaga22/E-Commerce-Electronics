using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Commands.CategoryFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using MediatR;
using ECommerceElctronics.DataServices.Services.Caching;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.CategoryFolder
{
    public class UpdateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheSerivces cacheSerivces) : IRequestHandler<UpdateCategoryCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ICacheSerivces _cacheSerivces = cacheSerivces;


        public async Task<Result<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Categories.Update(request.CategoryRequest);
            await _unitOfWork.CompleteAsync();

            var cacheKey = $"category-{request.CategoryRequest.Id}";
            _cacheSerivces.RemoveData(cacheKey);

            return Result<bool>.Success(true);
        }
    }
}
