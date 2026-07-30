using AutoMapper;
using ECommerceElctronics.Api.CQRS.Queries.BrandFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ECommerceElctronics.Api.CQRS.Handlers.BrandFolder
{
    public class GetByIdBrandHandler : IRequestHandler<GetByIdBrandQuery, Brand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByIdBrandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Brand> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.Brands.GetById(request.BrandId);

            if (brand == null)
                return null;

            return brand;
        }
    }
}
