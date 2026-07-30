using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Commands.BrandFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.BrandFolder
{
    public class UpdateBrandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateBrandCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<bool>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Brands.Update(request.BrandRequest);
            await _unitOfWork.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}
