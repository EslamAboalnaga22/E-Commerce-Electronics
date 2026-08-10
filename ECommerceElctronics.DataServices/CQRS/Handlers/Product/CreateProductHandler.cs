using AutoMapper;
using ECommerceElctronics.DataServices.CQRS.Commands.ProductFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Dtos.Responses;
using ECommerceElctronics.Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OutputCaching;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.ProductFolder
{
    public class CreateProductHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment, IOutputCacheStore outputCacheStore) : IRequestHandler<CreateProductCommand, Result<GetProductDetailsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IWebHostEnvironment environment = environment;
        private readonly string ImagePath = $"{environment.WebRootPath}/images/products";
        private readonly IOutputCacheStore _outputCacheStore = outputCacheStore;

        public async Task<Result<GetProductDetailsResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var coverName = await SaveCover(request.ProductRequest.Cover);

            var product = _mapper.Map<Product>(request.ProductRequest);

            if (product == null)
                return Result<GetProductDetailsResponse>.Failure(new("Something Wrong In Data", "Created"));

            product.Image = coverName;

            await _unitOfWork.Products.Add(product);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<GetProductDetailsResponse>(product);

            await _outputCacheStore.EvictByTagAsync("Products", cancellationToken);

            return Result<GetProductDetailsResponse>.Success(result);
        }

        private async Task<string> SaveCover(IFormFile cover)
        {

            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.Name)}";

            var path = Path.Combine(ImagePath, coverName);

            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);

            return coverName;
        }
    }
}
