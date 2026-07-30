using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Queries.BrandFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.BrandFolder
{
    public class GetAllBrandsHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllBrandsQuery, Result<IEnumerable<Brand>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<Brand>>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands = await _unitOfWork.Brands.GetAll();

            if (brands == null)
                return Result <IEnumerable<Brand>>.Failure(new("No Brands Found", "Readed"));

            return Result<IEnumerable<Brand>>.Success(brands);
        }
    }
}
