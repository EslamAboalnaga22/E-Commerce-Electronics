using AutoMapper;
using ECommerceElctronics.Api.CQRS.Commands.ProductFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.ProductFolder
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment environment;
        private readonly string ImagePath;

        public DeleteProductHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this.environment = environment;
            ImagePath = $"{environment.WebRootPath}/images/products";
        }
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var isDeleted = false;

            var product = await _unitOfWork.Products.GetById(request.ProductId);

            if (product == null)
                return false;

            await _unitOfWork.Products.Delete(product);
            var effectedRows = await _unitOfWork.CompleteAsync();

            if (effectedRows) // if product id deleted -> remove pic also from server
            {
                isDeleted = true;

                var cover = Path.Combine(ImagePath, product.Image);
                File.Delete(cover);
            }

            return true;
        }
    }
}
