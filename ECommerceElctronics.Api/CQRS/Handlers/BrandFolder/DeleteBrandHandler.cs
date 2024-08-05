using AutoMapper;
using ECommerceElctronics.Api.CQRS.Commands.BrandFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.BrandFolder
{
    public class DeleteBrandHandler : IRequestHandler<DeleteBrandCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteBrandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.Brands.GetById(request.BrandId);

            if (brand == null) 
                return false;

            await _unitOfWork.Brands.Delete(brand);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
