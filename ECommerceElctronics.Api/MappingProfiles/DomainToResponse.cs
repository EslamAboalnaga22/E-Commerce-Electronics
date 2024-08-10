using AutoMapper;
using ECommerceElctronics.Entities.Dtos.Requests;
using ECommerceElctronics.Entities.Dtos.Responses;
using ECommerceElctronics.Entities.Models;

namespace ECommerceElctronics.Api.MappingProfiles
{
    public class DomainToResponse : Profile
    {
        public DomainToResponse()
        {
            // ( Source , Destination )

            CreateMap<Product, GetProductDetailsResponse>()
                .ForMember(
                   dest => dest.Brand,
                   opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(
                   dest => dest.Category,
                   opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Cart, GetCartDetailsResponse>();

            CreateMap<Order, GetOrderDetailssResponse>()
                .ForMember(
                   dest => dest.Product,
                   opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(
                   dest => dest.UserName,
                   opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
                .ForMember(
                   dest => dest.OrderId,
                   opt => opt.MapFrom(src => src.Id));

            CreateMap<User, GetUserDetailsResponse>();
        }

    }
}
