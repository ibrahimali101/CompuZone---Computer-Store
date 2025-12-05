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
        // =========================================================
        // 1. CATEGORY MAPPINGS
        // =========================================================
        CreateMap<ReqCategoryDto, Category>();
        CreateMap<Category, ResCategoryDto>();

        // =========================================================
        // 2. CUSTOMER MAPPINGS
        // =========================================================
        CreateMap<ReqCustomerDto, Customer>();
        CreateMap<Customer, ResCustomerDto>();

        // =========================================================
        // 3. PRODUCT & IMAGES MAPPINGS
        // =========================================================

        // --- Images ---
        CreateMap<ReqProductImageDto, ProductImage>();
        CreateMap<ProductImage, ResProductImageDto>();

        // --- Products ---
        CreateMap<ReqProductDto, Product>();

        CreateMap<Product, ResProductDto>()
            // Flattening: Grab the name from the related Category object
            .ForMember(dest => dest.CategoryName,
                       opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : "N/A"))
            // AutoMapper automatically maps List<ProductImage> -> List<ResProductImageDto>
            // because we defined the Image map above.
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));

        // =========================================================
        // 4. SHIPPING & PAYMENT MAPPINGS
        // =========================================================
        CreateMap<ReqShippingDto, Shipping>();
        CreateMap<Shipping, ResShippingDto>();

        CreateMap<ReqPaymentDto, Payment>();
        CreateMap<Payment, ResPaymentDto>();

        // =========================================================
        // 5. ORDER & ORDER ITEMS (The Complex Part)
        // =========================================================

        // --- Order Items ---
        CreateMap<ReqOrderItemDto, OrderItem>();

        CreateMap<OrderItem, ResOrderItemDto>()
            // Flattening: Grab the Product Name for the display
            .ForMember(dest => dest.ProductName,
                       opt => opt.MapFrom(src => src.Product.ProductName))
            // Ensure Total is mapped (even though it's calculated in Entity, we want it in DTO)
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total));

        // --- Orders (ReqDto -> Entity) ---
        CreateMap<ReqOrderDto, Order>()
            // Map the list of items
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
            // Map the nested Shipping/Payment objects
            // Assuming your DTO calls it "ShippingDetails" but Entity calls it "Shipping"
            .ForMember(dest => dest.Shipping, opt => opt.MapFrom(src => src.ShippingDetails))
            .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.PaymentDetails));

        // --- Orders (Entity -> ResDto) ---
        CreateMap<Order, ResOrderDto>() // Or ResOrderDto
                                          // Flattening: Show Customer Name instead of just ID
            .ForMember(dest => dest.CustomerName,
                       opt => opt.MapFrom(src => src.Customer.Name))
            // Map the related objects so the full tree is returned
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
            .ForMember(dest => dest.Shipping, opt => opt.MapFrom(src => src.Shipping))
            .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Payment));
    }
}