using AutoMapper;
using TallerWeb.Src.Models;
using TallerWeb.Src.DTOs.Product;
using TallerWeb.Src.DTOs.Receipt;
using TallerWeb.Src.Service.Interfaces;

namespace TallerWeb.Src.Service.Implements
{
    public class MapperService : IMapperService
    {
        private readonly IMapper _mapper;

        public MapperService(IMapper mapper)
        {
            _mapper = mapper;
        }
       
       public Product ProductDtoToProduct(ProductDto productDto)
        {
            var mappedProduct = _mapper.Map<Product>(productDto);
            return mappedProduct;
        }

        public ProductDto ProductToProductDto(Product product)
        {
            var mappedProductDto = _mapper.Map<ProductDto>(product);
            return mappedProductDto;
        }

        public ReceiptDto ReceiptToReceiptDto(Receipt receipt)
        {
            var mappedReceiptDto = _mapper.Map<ReceiptDto>(receipt);
            return mappedReceiptDto;
        }

        public Receipt ReceiptDtoToReceipt(ReceiptDto receiptDto)
        {
            var mappedReceipt = _mapper.Map<Receipt>(receiptDto);
            return mappedReceipt;
        }
    }
}