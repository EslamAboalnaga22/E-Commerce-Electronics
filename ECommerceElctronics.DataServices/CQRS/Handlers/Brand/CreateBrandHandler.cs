using AutoMapper;
using ECommerceElctronics.DataServices.CQRS.Commands.BrandFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.Services.Caching;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.BrandFolder
{
    public class CreateBrandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheSerivces cacheSerivces) : IRequestHandler<CreateBrandCommand, Result<Brand>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ICacheSerivces _cacheSerivces = cacheSerivces;

        public async Task<Result<Brand>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<Brand>(request.BrandRequest);

            if (result == null)
                return Result<Brand>.Failure(new("Something Wrong In Data","Created")) ;

            await _unitOfWork.Brands.Add(result);
            await _unitOfWork.CompleteAsync();

            var cacheKey = $"all-brands";
            _cacheSerivces.RemoveData(cacheKey);

            return Result<Brand>.Success(result);
        }
    }
}
