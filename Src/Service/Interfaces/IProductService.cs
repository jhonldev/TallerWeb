using TallerWeb.Src.DTOs.Product;
using TallerWeb.Src.DTOs.Receipt;

namespace TallerWeb.Src.Service.Interfaces{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts(string query);

        Task<IEnumerable<ProductDto>> GetProductsAvailable();

        Task<bool> CreateProduct(ProductDto productDto, IFormFile photo);

        Task<bool> UpdateProduct(int id, ProductUpdateDto productDto);

        Task<bool> DeleteProduct(int id);

        Task<ReceiptDto?> BuyProduct(ProductBuyDto productBuyDto, string userId);
    }
}