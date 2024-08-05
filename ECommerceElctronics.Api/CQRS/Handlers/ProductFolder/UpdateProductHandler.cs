using AutoMapper;
using ECommerceElctronics.Api.CQRS.Commands.ProductFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.ProductFolder
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment environment;
        private readonly string ImagePath;

        public UpdateProductHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this.environment = environment;
            ImagePath = $"{environment.WebRootPath}/images/products";
        }
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var p = await _unitOfWork.Products.GetById(request.ProductIdRequest);

            var product = _mapper.Map<Product>(request.ProductRequest);

            product.Id = request.ProductIdRequest;

            var hasNewCover = request.ProductRequest.Cover != null;
            var oldCover = p.Image;

            // will update photo
            if (hasNewCover)
                product.Image = await SaveCover(request.ProductRequest.Cover!);

            await _unitOfWork.Products.Update(product);
            var effectedRows = await _unitOfWork.CompleteAsync();

            if (effectedRows)
            {
                if (hasNewCover) // will remove old picture
                {
                    var cover = Path.Combine(ImagePath, oldCover);
                    File.Delete(cover);
                }

                return true;
            }
            else // if update no complete and pic save id server -> this remove pic from server
            {
                var cover = Path.Combine(ImagePath, product.Image);
                File.Delete(cover);
                return false;
            }
        }

        private async Task<string> SaveCover(IFormFile cover)
        {

            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";

            var path = Path.Combine(ImagePath, coverName);

            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);

            return coverName;
        }
    }
}
