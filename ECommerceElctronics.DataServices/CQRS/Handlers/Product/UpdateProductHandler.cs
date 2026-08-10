using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Commands.ProductFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OutputCaching;

namespace ECommerceElctronics.DataServicesDataServices.CQRS.Handlers.ProductFolder
{
    public class UpdateProductHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment, IOutputCacheStore outputCacheStore) : IRequestHandler<UpdateProductCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IWebHostEnvironment environment = environment;
        private readonly string ImagePath = $"{environment.WebRootPath}/images/products";
        private readonly IOutputCacheStore _outputCacheStore = outputCacheStore;

        public async Task<Result<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
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

                await _outputCacheStore.EvictByTagAsync("Products", cancellationToken);

                return Result<bool>.Success(true);
            }
            else // if update no complete and pic save id server -> this remove pic from server
            {
                var cover = Path.Combine(ImagePath, product.Image);
                File.Delete(cover);

                await _outputCacheStore.EvictByTagAsync("Products", cancellationToken);

                return Result<bool>.Failure(new("Remove Picture From Server", "Photo"));
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
