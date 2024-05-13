using TallerWeb.Src.Models;
using TallerWeb.Src.DTOs.Product;
using TallerWeb.Src.DTOs.Receipt;

namespace TallerWeb.Src.Service.Interfaces
{
    public interface IMapperService
    {
        Product ProductDtoToProduct(ProductDto productDto);

        ProductDto ProductToProductDto(Product product);

        ReceiptDto ReceiptToReceiptDto(Receipt receipt);

        Receipt ReceiptDtoToReceipt(ReceiptDto receiptDto);
    }
}