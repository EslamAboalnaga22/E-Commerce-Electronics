using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Commands.BrandFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using MediatR;
using ECommerceElctronics.DataServices.Services.Caching;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.BrandFolder
{
    public class UpdateBrandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheSerivces cacheSerivces) : IRequestHandler<UpdateBrandCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ICacheSerivces _cacheSerivces = cacheSerivces;

        public async Task<Result<bool>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Brands.Update(request.BrandRequest);
            await _unitOfWork.CompleteAsync();

            var cacheKey = $"brand-{request.BrandRequest.Id}";
            _cacheSerivces.RemoveData(cacheKey);

            return Result<bool>.Success(true);
        }
    }
}
