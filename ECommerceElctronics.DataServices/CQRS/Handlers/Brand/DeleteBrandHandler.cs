using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Commands.BrandFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.BrandFolder
{
    public class DeleteBrandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<DeleteBrandCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<bool>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.Brands.GetById(request.BrandId);

            if (brand == null)
                return Result<bool>.Failure(new("Brand Not Found", "Deleted"));

            await _unitOfWork.Brands.Delete(brand);
            await _unitOfWork.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}
