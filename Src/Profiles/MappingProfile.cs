using TallerWeb.Src.Models;
using AutoMapper;
using TallerWeb.Src.DTOs.Product;
using TallerWeb.Src.DTOs.Receipt;

namespace ayudantia_IDWM.Src.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
            CreateMap<Receipt, ReceiptDto>();
            CreateMap<ReceiptDto, Receipt>();
        }
    }
}