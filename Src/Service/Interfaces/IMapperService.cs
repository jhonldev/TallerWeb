using TallerWeb.Src.Models;
using TallerWeb.Src.DTOs.Product;

namespace TallerWeb.Src.Service.Interfaces
{
    public interface IMapperService
    {
        Product ProductDtoToProduct(ProductDto productDto);

        ProductDto ProductToProductDto(Product product);
    }
}