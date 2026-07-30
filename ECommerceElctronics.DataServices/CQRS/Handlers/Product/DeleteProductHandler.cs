using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Commands.ProductFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.ProductFolder
{
    public class DeleteProductHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment) : IRequestHandler<DeleteProductCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IWebHostEnvironment environment = environment;
        private readonly string ImagePath = $"{environment.WebRootPath}/images/products";

        public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var isDeleted = false;

            var product = await _unitOfWork.Products.GetById(request.ProductId);

            if (product == null)
                return Result<bool>.Failure(new("Product Not Found", "Deleted"));

            await _unitOfWork.Products.Delete(product);
            var effectedRows = await _unitOfWork.CompleteAsync();

            if (effectedRows) // if product id deleted -> remove pic also from server
            {
                isDeleted = true;

                var cover = Path.Combine(ImagePath, product.Image);
                File.Delete(cover);
            }

            return Result<bool>.Success(true);
        }
    }
}
