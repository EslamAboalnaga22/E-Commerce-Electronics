using AutoMapper;
using ECommerceElctronics.Entities.Dtos.Payment;
using ECommerceElctronics.Entities.Dtos.Requests;
using ECommerceElctronics.Entities.Models;

namespace ECommerceElctronics.Api.MappingProfiles
{
    public class RequestToDomain : Profile
    {
        public RequestToDomain()
        {
            // ( Source , Destination )

            CreateMap<CreateBrandRequest, Brand>();
                //.ForMember(src => src.Products, opt => opt.Ignore());

            CreateMap<CreateCategoryRequest, Category>();

            CreateMap<CreateProductRequest, Product>()
                .ForMember(src => src.Image, opt => opt.Ignore());

            CreateMap<UpdateProductRequest, Product>()
               .ForMember(src => src.Image, opt => opt.Ignore());

            CreateMap<UpdateOrderRequest, Order>();
        }
    }
}
