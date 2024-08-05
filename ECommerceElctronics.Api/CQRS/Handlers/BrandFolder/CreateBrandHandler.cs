using AutoMapper;
using ECommerceElctronics.Api.CQRS.Commands.BrandFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.BrandFolder
{
    public class CreateBrandHandler : IRequestHandler<CreateBrandCommand, Brand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBrandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Brand> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<Brand>(request.BrandRequest);

            await _unitOfWork.Brands.Add(result);
            await _unitOfWork.CompleteAsync();

            if (request.BrandRequest == null)
                return null;

            return result;
        }
    }
}
