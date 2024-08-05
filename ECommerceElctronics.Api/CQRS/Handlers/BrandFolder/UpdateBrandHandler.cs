using AutoMapper;
using ECommerceElctronics.Api.CQRS.Commands.BrandFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.BrandFolder
{
    public class UpdateBrandHandler : IRequestHandler<UpdateBrandCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBrandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Brands.Update(request.BrandRequest);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
