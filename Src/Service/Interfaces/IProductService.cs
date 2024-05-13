using TallerWeb.Src.DTOs.Product;
using TallerWeb.Src.DTOs.Receipt;

namespace TallerWeb.Src.Service.Interfaces{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();

        Task<IEnumerable<ProductDto>> GetProductsAvailable();

        Task<bool> CreateProduct(ProductDto productDto);

        Task<bool> UpdateProduct(int id, ProductUpdateDto productDto);

        Task<bool> DeleteProduct(int id);

        Task<ReceiptDto?> BuyProduct(ProductBuyDto productBuyDto);

        Task<IEnumerable<ReceiptDto>> GetReceipts();
    }
}