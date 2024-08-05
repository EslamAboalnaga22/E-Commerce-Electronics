using AutoMapper;
using ECommerceElctronics.Api.CQRS.Commands.BrandFolder;
using ECommerceElctronics.Api.CQRS.Commands.ProductFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using ECommerceElctronics.Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace ECommerceElctronics.Api.CQRS.Handlers.ProductFolder
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, GetProductDetailsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment environment;
        private readonly string ImagePath;

        public CreateProductHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this.environment = environment;
            ImagePath = $"{environment.WebRootPath}/images/products";
        }

        public async Task<GetProductDetailsResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var coverName = await SaveCover(request.ProductRequest.Cover);

            var product = _mapper.Map<Product>(request.ProductRequest);

            product.Image = coverName;

            await _unitOfWork.Products.Add(product);
            await _unitOfWork.CompleteAsync();

            if (product == null)
                return null;

            var result = _mapper.Map<GetProductDetailsResponse>(product);

            return result;
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
