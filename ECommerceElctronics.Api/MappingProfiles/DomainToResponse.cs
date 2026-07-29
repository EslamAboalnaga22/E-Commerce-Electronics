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

            CreateMap<Cart, CartDtoResponse>()
                .ForMember(
                   dest => dest.TotalPrice,
                   opt => opt.MapFrom(src => src.Items.Sum(i => i.Quantity * i.UnitPrice)));

            CreateMap<CartItem, CartItemsDtoResponse>();

            CreateMap<Order, OrderDtoResponse>();

            CreateMap<OrderItem, OrderItemDto>();

            CreateMap<User, GetUserDetailsResponse>();
        }

    }
}
