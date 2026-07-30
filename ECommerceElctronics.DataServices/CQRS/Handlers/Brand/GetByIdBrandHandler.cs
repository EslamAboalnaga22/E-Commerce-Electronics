using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Queries.BrandFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.BrandFolder
{
    public class GetByIdBrandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetByIdBrandQuery, Result<Brand>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<Brand>> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.Brands.GetById(request.BrandId);

            if (brand == null)
                return Result<Brand>.Failure(new("No Brand Found", "Readed"));

            return Result<Brand>.Success(brand);
        }
    }
}
