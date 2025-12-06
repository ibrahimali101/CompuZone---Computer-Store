using AutoMapper;
using CompuZone.DAL.Entities;
// Adjust these namespaces to match where you actually put your DTO files
using CompuZone.BLL.DTOs.Category;
using CompuZone.BLL.DTOs.Customer;
using CompuZone.BLL.DTOs.Product;
using CompuZone.BLL.DTOs.Order;
using CompuZone.BLL.DTOs.Shipping;
using CompuZone.BLL.DTOs.Payment;
using CompuZone.BLL.DTOs.ProductImage;
using CompuZone.BLL.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ReqCategoryDto, Category>();
        CreateMap<Category, ResCategoryDto>();

        CreateMap<ReqCustomerDto, Customer>();
        CreateMap<Customer, ResCustomerDto>();

        CreateMap<ReqProductImageDto, ProductImage>();
        CreateMap<ProductImage, ResProductImageDto>();

        CreateMap<ReqProductDto, Product>();
        CreateMap<Product, ResProductDto>()
            .ForMember(dest => dest.CategoryName,
                       opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : "N/A"))
            .ForMember(dest => dest.Images,
                       opt => opt.MapFrom(src => src.Images));

        CreateMap<ReqShippingDto, Shipping>();
        CreateMap<Shipping, ReqShippingDto>();

        CreateMap<ReqPaymentDto, Payment>();
        CreateMap<Payment, ResPaymentDto>();

        CreateMap<ReqOrderItemDto, OrderItem>();

        CreateMap<OrderItem, ResOrderItemDto>()
            .ForMember(dest => dest.ProductName,
                       opt => opt.MapFrom(src => src.Product.ProductName))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total));

        CreateMap<ReqOrderDto, Order>()
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
            .ForMember(dest => dest.Shipping, opt => opt.MapFrom(src => src.ShippingDetails))
            .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.PaymentDetails));

        CreateMap<Order, ResOrderDto>()
            .ForMember(dest => dest.CustomerName,
                       opt => opt.MapFrom(src => src.Customer.Name))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
            .ForMember(dest => dest.Shipping, opt => opt.MapFrom(src => src.Shipping))
            .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Payment));
    }
}