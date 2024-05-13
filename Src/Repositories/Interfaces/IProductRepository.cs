using TallerWeb.Src.Models;
using TallerWeb.Src.DTOs.Product;

namespace TallerWeb.Src.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();

        Task<IEnumerable<Product>> GetProductsAvailable();

        Task<bool> CreateProduct(Product product);

        Task<bool> ExistingProduct(string name, string type);

        Task<bool> UpdateProduct(int id, ProductUpdateDto productDto);

        Task<bool> DeleteProduct(int id);

        Task<int> BuyProduct(ProductBuyDto productBuyDto);

        Task<int> GetStock(string name, string type);

        Task<Receipt> GenerateReceipt(Receipt receipt);

        Task<int> PriceProduct(string name, string type);

        Task<IEnumerable<Receipt>> GetReceipts();
    }
}